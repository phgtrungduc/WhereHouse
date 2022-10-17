
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
        ServiceResult ChangePassword(string userId, string oldPassword, string newPassword);


    }
}
