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

namespace PTDuc.WhereHouse.BL.BusinessLayer.Login
{
    public class BLLogin : BLBase<LoginParam, LoginParam>, IBLLogin
    {
        IAuthenticationManager _authenticationManager;
        IDLUser _dlUser;
        public BLLogin(IDLLogin dLLogin, IAuthenticationManager authenticationManager,IMapper mapper,IDLUser dLUser) : base(dLLogin, mapper)
        {
            _authenticationManager = authenticationManager;
            _dlUser = dLUser;
        }

        public bool ChangePassword(string username, string oldPassword, string newPassword, ServiceResult serviceResult)
        {
            //var loginEntity = _loginRepository.Login(username);
            //if (loginEntity != null)
            //{
            //    var isPasswordOk = CommonFunction.validatePassword(oldPassword, loginEntity.Password, loginEntity.Salt);
            //    if (isPasswordOk)
            //    {
            //        var passObject = CommonFunction.hashPassWord(newPassword);
            //        var newPass = passObject.GetType().GetProperty("Password").GetValue(passObject);
            //        var salt = passObject.GetType().GetProperty("Salt").GetValue(passObject);
            //        var updateRows = _loginRepository.UpdatePasswordByEmployeeCode(username, newPass.ToString(), salt.ToString());
            //        if (updateRows > 0)
            //        {
            //            serviceResult.Messenger = "Đổi mật khẩu thành công";
            //            //serviceResult.StatusCode = Enums.StatusCode.Success;
            //            return true;
            //        }
            //        else
            //        {
            //            serviceResult.Messenger = "Đổi mật khẩu thất bại";
            //            //serviceResult.StatusCode = Enums.StatusCode.NotValid;
            //            return false;
            //        }
            //    }
            //    else
            //    {
            //        serviceResult.Messenger = "Mật khẩu cũ không chính xác";
            //        serviceResult.Data = null;
            //        //serviceResult.StatusCode = Enums.StatusCode.NotValid;
            //        return false;
            //    }
            //}
            //else
            //{
            //    serviceResult.Data = null;
            //    //serviceResult.StatusCode = Enums.StatusCode.NotValid;
            //    serviceResult.Messenger = "Username không tồn tại";
            //    return false;
            //}
            return true;
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
                        var accessToken = _authenticationManager.Authenticate(entity.UserId.ToString(),entity.UserName, entity.Password);
                        res.Data = new { accessToken = accessToken,User = entity };
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