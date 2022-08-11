using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.BL.Interfaces.Login;
using PTDuc.WhereHouse.DL.Interfaces.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels;
using System.Reflection;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using AutoMapper;
using PTDuc.WhereHouse.EntityModels.DTO;

namespace PTDuc.WhereHouse.BL.BusinessLayer.Login
{
    public class BLLogin : BLBase<LoginParam, LoginParam>, IBLLogin
    {
        IAuthenticationManager _authenticationManager;
        IDLUser _dlUser;
        IBLUser _bLUser;
        public BLLogin(IDLLogin dLLogin, IAuthenticationManager authenticationManager, IMapper mapper, IDLUser dLUser, IBLUser bLUser) : base(dLLogin, mapper)
        {
            _authenticationManager = authenticationManager;
            _dlUser = dLUser;
            _bLUser = bLUser;
        }

        public ServiceResult ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var res = new ServiceResult();
            var user = _bLUser.GetByID(userId);
            if (user != null)
            {
                var passCheck = new LoginParam() { Password = oldPassword, HashPassword = user.Password, Salt = user.Salt };
                if (PasswordHash.Verify(passCheck))
                {
                    var loginParam = PasswordHash.HashPassword(newPassword);
                    user.Password = loginParam.HashPassword;
                    user.Salt = loginParam.Salt;
                    var userUpdate = _mapper.Map<UserDTO>(user);
                    var success = _bLUser.Update(userUpdate,userId);
                    if (success)
                    {
                        var accessToken = _authenticationManager.Authenticate(user.UserId.ToString(), user.UserName, newPassword);
                        res.Data = new { accessToken = accessToken, User = user };
                    }
                    else {
                        res.Data = false;
                    }
                    
                }
                else
                {
                    res.StatusCode = (int)Enumeration.ResultCode.PasswordNotCorrect;
                }
            }
            return res;
        }

        public ServiceResult Login(LoginParam param)
        {
            var res = new ServiceResult();
            if (!string.IsNullOrEmpty(param.Username))
            {
                var entity = _dlUser.GetUserByUserName(param.Username);
                if (entity != null)
                {
                    param.HashPassword = entity.Password;
                    param.Salt = entity.Salt;
                    if (PasswordHash.Verify(param))
                    {
                        var accessToken = _authenticationManager.Authenticate(entity.UserId.ToString(), entity.UserName, entity.Password);
                        res.Data = new { accessToken = accessToken, User = entity };
                    }
                    else
                    {
                        res.StatusCode = (int)Enumeration.ResultCode.PasswordNotCorrect;
                    }
                }
                else
                {
                    res.StatusCode = (int)Enumeration.ResultCode.UserNotFound;
                }
            }
            else
            {
                //param chưa chính xác
                res.StatusCode = (int)Enumeration.ResultCode.NotTrueParam;
            }
            //if ()
            return res;
        }

    }
}