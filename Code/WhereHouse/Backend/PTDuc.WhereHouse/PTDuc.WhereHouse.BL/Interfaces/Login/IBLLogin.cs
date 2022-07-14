using PTDuc.WhereHouse.DL.Models;
using PTDuc.WhereHouse.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.BL.Interfaces.Login
{
    public interface IBLLogin : IBLBase<LoginParam,LoginParam>
    {
        ServiceResult Login(LoginParam entity);
        bool ChangePassword(string username, string oldPassword, string newPassword, ServiceResult serviceResult);


    }
}
