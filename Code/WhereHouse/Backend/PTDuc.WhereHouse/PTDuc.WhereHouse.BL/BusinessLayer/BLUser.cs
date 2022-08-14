using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLUser : BLBase<User,UserDTO>,IBLUser
    {
        IDLUser _dlUser;
        List<UserDTO> userOnline;
        public BLUser(IDLUser dLUser, IMapper mapper) : base(dLUser, mapper)
        {
            _dlUser = dLUser;
            userOnline = new List<UserDTO>();
        }

        public override bool BeforeInsert(ref UserDTO entity)
        {
            var res = false;
            if (!string.IsNullOrEmpty(entity.Password)) {
                LoginParam loginParam = new LoginParam();
                loginParam = PasswordHash.HashPassword(entity.Password);
                if (loginParam != null) {
                    entity.Password = loginParam.HashPassword;
                    entity.Salt = loginParam.Salt;
                    entity.CreatedDate = DateTime.Now;
                    res =  true;
                }
            }
            return res;
        }
        public void AddUserToListOnline(UserDTO user)
        {
            if (user != null) {
                var isExist =  userOnline.FindIndex(x => x.UserId == user.UserId);
                if (isExist == -1) {
                    userOnline.Add(user);
                }
            }
        }

        public void RemoveUserFromListOnline(UserDTO user)
        {
            if (user != null) {
                userOnline.RemoveAll(x => x.UserId == user.UserId);
            }
        }

        public List<UserDTO> GetListUserOnline()
        {
            return this.userOnline;
        }

        public ServiceResult InsertAdmin(UserDTO user, string adminId)
        {
            var res = new ServiceResult();
            var admin = this.GetByID(adminId);
            try
            {
                if (admin != null)
                {
                    if (admin.Role == (int)Enumeration.Role.Admin)
                    {
                        user.Role = (int)Enumeration.Role.Admin;
                        res.Data = this.Insert(user);
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

        public ServiceResult ChangeStatus(string blockUserId, string adminId)
        {
            var res = new ServiceResult();
            var admin = this.GetByID(adminId);
            try
            {
                if (admin != null)
                {
                    if (admin.Role == (int)Enumeration.Role.Admin)
                    {
                        var user = this.GetByID(blockUserId);
                        if (user != null)
                        {
                            if (user.Status != (int)Enumeration.StatusUser.Blocked)
                            {
                                user.Status = (int)Enumeration.StatusUser.Blocked;
                                this.Update(user, blockUserId);
                                res.Data = true;
                                res.Messenger = "Chặn người dùng thành công";
                            }
                            else {
                                user.Status = (int)Enumeration.StatusUser.Active;
                                this.Update(user, blockUserId);
                                res.Data = true;
                                res.Messenger = "Bỏ chặn người dùng thành công";
                            }
                            
                        }
                        else {
                            res.StatusCode = (int)Enumeration.ResultCode.Failed;
                            res.Messenger = "Không tồn tại người dùng";
                        }
                        
                    }
                    else
                    {
                        res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                        res.Messenger = "Tài khoản không có quyền chặn người dùng";
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

        public ServiceResult GetListUserForAdmin(string adminId)
        {
            var res = new ServiceResult();
            var admin = this.GetByID(adminId);
            try
            {
                if (admin != null)
                {
                    if (admin.Role == (int)Enumeration.Role.Admin)
                    {
                        var listUser = _dlUser.GetAllForAdmin();
                        listUser = listUser.Where(x=>x.Role==(int)Enumeration.Role.User)?.ToList();
                        res.Data = listUser;
                    }
                    else
                    {
                        res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                        res.Messenger = "Tài khoản không có quyền quản trị hệ thống";
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
        public override UserDTO GetByID(string Id)
        {
            var user = base.GetByID(Id);
            var userDTO = _mapper.Map<UserDTO>(user);
            if (user.Avatar != null)
            {
                userDTO.AvatarPath = user.Avatar.FilePath;
            }
            return userDTO;
        }

        public ServiceResult UpdateUser(string userId, UserDTO user)
        {
            var res = new ServiceResult();
            var userInfo = this.GetByID(userId);
            if (userInfo != null && userInfo.UserId.ToString() == userId)
            {
                userInfo.FullName = user.FullName;
                userInfo.Email = user.Email;
                userInfo.AvatarId = user.AvatarId;
                userInfo.DistrictCode = user.DistrictCode;
                userInfo.WardCode = user.WardCode;
                userInfo.ProvinceCode = user.ProvinceCode;
                userInfo.PhoneNumber = user.PhoneNumber;
                var isSucess = this.Update(userInfo, userId);
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
            else {
                res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                res.Data = false;
            }
            return res;
        }

        public ServiceResult InsertUser(UserDTO user)
        {
            var res = new ServiceResult();
            try
            {
                var entity = _dlUser.GetUserByUserName(user.UserName);
                if (entity == null)
                {
                    res.Data = this.Insert(user);
                }
                else {
                    res.Data = false;
                    res.StatusCode = (int)(Enumeration.ResultCode.UserExist);
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
