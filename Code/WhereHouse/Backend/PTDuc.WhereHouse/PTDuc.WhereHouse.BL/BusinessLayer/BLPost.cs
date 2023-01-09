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
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLPost : BLBase<Post, PostDTO>, IBLPost
    {
        IDLPost _DLPost;
        IDLHouse _dLHouse;
        IBLUser _BLUser;
        IDLReport _dLReport;
        public BLPost(IDLPost DLPost, IMapper mapper, IDLHouse dLHouse, IBLUser BLUser, IDLReport dLReport) : base(DLPost, mapper)
        {
            _DLPost = DLPost;
            _dLHouse = dLHouse;
            _BLUser = BLUser;
            _dLReport = dLReport;
        }


        public ServiceResult GetUserPost(Guid userId)
        {
            var res = new ServiceResult();
            var data = _DLPost.GetUserPost(userId);
            var dataDTO = _mapper.Map<List<PostDTO>>(data);
            dataDTO.ForEach(post =>
            {
                if (post.House != null && post.House.HouseImage != null) {
                    post.HouseImageUrl = post.House.HouseImage.FilePath;
                }
            });
            res.Data = dataDTO;
            return res;
        }
        public ServiceResult DeletePostUser(string postId, string userId)
        {
            var res = new ServiceResult();
            res.Data = false;
            var post = _DLPost.GetByID(postId);
            if (post != null)
            {
                if (post.UserId.ToString() == userId || post.User.Role == 2)
                {
                    var delSuccess = _DLPost.DeleteById(postId);
                    if (delSuccess)
                    {
                        delSuccess = _dLHouse.DeleteById(post.House.HouseId.ToString());
                        if (delSuccess)
                        {
                            res.Data = true;
                        }
                        else
                        {
                            res.Messenger = "Xóa thông tin bài đăng";
                        }
                    }
                    else
                    {
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
                        var isSuccess = this.Update(post,postId);
                        if (isSuccess)
                        {
                            res.Data = true;
                        }
                        else {
                            res.Data = false;
                            res.StatusCode = (int)Enumeration.ResultCode.Failed;
                        }
                    }
                    else
                    {
                        res.StatusCode = (int)Enumeration.ResultCode.PostNotPay;
                        res.Messenger = "Không thể phê duyệt bài đăng chưa được thanh toán";
                    }
                }
            }
            else
            {
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
                else
                {
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
                    var listPost = _DLPost.GetListPostForAdmin();
                    var listPostDTO = _mapper.Map<List<PostDTO>>(listPost);
                    listPostDTO.ForEach(x =>
                    {
                        if (x.User != null)
                        {
                            x.UserName = x.User.UserName;
                        }
                    });
                    res.Data = listPostDTO;
                }
            }
            catch (Exception ex)
            {
                res.Messenger = ex.Message;
                res.StatusCode = (int)(Enumeration.ResultCode.Failed);
            }
            return res;
        }
        public override PostDTO GetByID(string Id)
        {
            var post = base.GetByID(Id);
            var postDTO = _mapper.Map<PostDTO>(post);
            if (post.House != null && post.House.HouseImage != null)
            {
                postDTO.HouseImageUrl = post.House.HouseImage.FilePath;
            }
            return postDTO;
        }

        public ServiceResult ReportPost(string userId, ReportDTO report)
        {
            var res = new ServiceResult();
            var userReport = _dLReport.UserHasReported(Guid.Parse(userId), report.PostId);
            if (userReport == null)
            {
                var post = this.GetByID(report.PostId.ToString());
                if (post != null)
                {
                    if (post.UserId.ToString() != userId)
                    {
                        var dataForInsert = _mapper.Map<Report>(report);
                        var success = _dLReport.Insert(dataForInsert);
                        if (success)
                        {
                            res.Data = true;
                        }
                        else
                        {
                            res.StatusCode = (int)ResultCode.Failed;
                            res.Data = false;
                        }
                    }
                    else
                    {
                        res.StatusCode = (int)ResultCode.NotHaveRight;
                        res.Data = false;
                    }
                }
            }
            else
            {
                res.Data = false;
                res.StatusCode = (int)ResultCode.HasReport;
            }
            return res;
        }

        public ServiceResult ChangeStatusReport(string userId, string reportId)
        {
            var res = new ServiceResult();
            var user = _BLUser.GetByID(userId);
            if (user != null && user.Role == (int)Enumeration.Role.Admin)
            {
                var report = _dLReport.GetByID(reportId);
                if (report != null)
                {
                    if (report.Status == (int)Enumeration.StatusReport.Created)
                    {
                        report.Status = (int)Enumeration.StatusReport.Solved;
                    }
                    else
                    {
                        report.Status = (int)Enumeration.StatusReport.Created;
                    }
                    var isSucess = _dLReport.Update(report, reportId);
                    if (isSucess)
                    {
                        res.Data = true;
                    }
                    else
                    {
                        res.StatusCode = (int)Enumeration.ResultCode.Failed;
                        res.Data = false;
                    }
                }
            }
            else
            {
                res.Data = false;
                res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                res.Messenger = "Tài khoản không có quyền chuyển trạng thái báo cáo";
            }
            return res;
        }

        public ServiceResult GetPublicPost(string postId)
        {
            var res = new ServiceResult();
            var post = this.GetByID(postId);
            if (post.Status == (int)Enumeration.StatusPost.Accepted)
            {
                res.Data = post;
            }
            else {
                res.Data = false;
                res.StatusCode = (int)ResultCode.NotHaveRight;
            }
            return res;
        }

        public ServiceResult GetDetailPost(string postId, string userId)
        {
            var res = new ServiceResult();
            var post = this.GetByID(postId);
            if (post.Status == (int)Enumeration.StatusPost.Accepted)
            {
                res.Data = post;
            }
            else {
                var user = _BLUser.GetByID(userId);
                if (user != null) {
                    if (user.UserId.ToString() == userId || user.Role == (int)Role.Admin)
                    {
                        res.Data = post;
                    }
                    else {
                        res.Data = false;
                        res.StatusCode = res.StatusCode = (int)ResultCode.NotHaveRight;
                    }
                }
            }
            return res;
        }

        public List<PostDTO> GetSearchResult(string search)
        {
            var data =  _DLPost.GetSearchResult(search);
            var dataDTO = _mapper.Map<List<PostDTO>>(data);
            dataDTO.ForEach(post =>
            {
                if (post.House != null && post.House.HouseImage != null)
                {
                    post.HouseImageUrl = post.House.HouseImage.FilePath;
                }
            });
            return dataDTO;
        }
    }
}
