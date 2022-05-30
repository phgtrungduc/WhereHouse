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

namespace PTDuc.WhereHouse.BL.BusinessLayer.Login
{
    public class BLLogin : BLBase<LoginParam>, IBLLogin
    {
        IAuthenticationManager _authenticationManager;
        //public BLLogin(IDLLogin dLLogin) : base(dLLogin)
        //{

        //}
        public BLLogin(IDLLogin dLLogin, IAuthenticationManager authenticationManager) : base(dLLogin)
        {
            _authenticationManager = authenticationManager;
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
                var propUsername = typeof(LoginParam).GetProperty("Username");
                var entity = _dlBase.GetOneByKeyWithType<User>(propUsername, param);
                if (entity != null)
                {
                    param.HashPassword = entity.Password;
                    param.Salt = entity.Salt;
                    if (PasswordHash.Verify(param))
                    {
                        var accessToken = _authenticationManager.Authenticate(entity.UserName, entity.Password);
                        res.Data = new { accessToken = accessToken };
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