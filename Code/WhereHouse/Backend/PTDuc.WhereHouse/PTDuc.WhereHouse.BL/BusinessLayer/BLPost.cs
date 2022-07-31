using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using System;
using static PTDuc.WhereHouse.EntityModels.Enumeration;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLPost : BLBase<Post, PostDTO>, IBLPost
    {
        IDLPost _DLPost;
        IDLHouse _dLHouse;
        IBLUser _BLUser;
        public BLPost(IDLPost DLPost, IMapper mapper, IDLHouse dLHouse, IBLUser BLUser) : base(DLPost, mapper)
        {
            _DLPost = DLPost;
            _dLHouse = dLHouse;
            _BLUser = BLUser;
        }


        public ServiceResult GetUserPost(Guid userId)
        {
            var res = new ServiceResult();
            res.Data = _DLPost.GetUserPost(userId);
            return res;
        }
        public ServiceResult DeletePostUser(string postId, string userId)
        {
            var res = new ServiceResult();
            res.Data = false;
            var post = _DLPost.GetByID(postId);
            if (post != null)
            {
                if (post.UserId.ToString() == userId)
                {
                    var delSuccess = _DLPost.DeleteById(postId);
                    if (delSuccess)
                    {
                        delSuccess = _dLHouse.DeleteById(post.House.HouseId.ToString());
                        if (delSuccess)
                        {
                            res.Data = true;
                        }
                        else {
                            res.Messenger = "Xóa thông tin bài đăng";
                        }
                    }
                    else {
                        res.Messenger = "Xóa thông tin nhà thất bại";
                    }
                }
                else
                {
                    res.StatusCode = (int)ResultCode.NotHaveRight;
                }
            }
            else
            {
                res.StatusCode = (int)ResultCode.Failed;
            }
            return res;
        }

        public ServiceResult AcceptPost(string postId, string userId)
        {
            var res = new ServiceResult();
            var user = _BLUser.GetByID(userId);
            if (user != null && user.Role == (int)Enumeration.Role.Admin)
            {
                var post = this.GetByID(postId);
                if (post != null)
                {
                    if (post.Status == (int)Enumeration.StatusPost.Pay)
                    {
                        post.Status = (int)Enumeration.StatusPost.Accepted; 
                        res.Data = post;
                    }
                    else
                    {
                        res.StatusCode = (int)Enumeration.ResultCode.PostNotPay;
                        res.Messenger = "Không thể phê duyệt bài đăng chưa được thanh toán";
                    }
                }
            }
            else {
                res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                res.Messenger = "Tài khoản không có quyền phê duyệt bài đăng";
            }
            return res;
        }
        public ServiceResult ClosePost(string postId, string userId)
        {
            var res = new ServiceResult();
            var user = _BLUser.GetByID(userId);
            var post = this.GetByID(postId);
            if (post != null)
            {
                if ((user != null && user.Role == (int)Enumeration.Role.Admin) || post.UserId.ToString() == userId)
                {
                    post.Status = (int)Enumeration.StatusPost.Closed;
                    res.Data = post;
                }
                else {
                    res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                    res.Messenger = "Tài khoản không có quyền phê duyệt bài đăng";
                }
            }
            return res;
        }
        public ServiceResult ReportPost(string postId)
        {
            var res = new ServiceResult();
            var post = this.GetByID(postId);
            if (post != null)
            {
                
            }
            return res;
        }
        public ServiceResult GetListPostForAdmin(string adminId)
        {
            var res = new ServiceResult();
            var admin = _BLUser.GetByID(adminId);
            try
            {
                if (admin != null)
                {
                    if (admin.Role == (int)Enumeration.Role.Admin)
                    {
                        var listPost = _DLPost.GetListPostForAdmin();
                        res.Data = listPost;
                    }
                    else
                    {
                        res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                        res.Messenger = "Tài khoản không có quyền phê duyệt bài đăng";
                    }
                }
            }
            catch (Exception ex)
            {
                res.Messenger = ex.Message;
                res.StatusCode = (int)(Enumeration.ResultCode.Failed);
            }
            return res;
        }
    }
}
