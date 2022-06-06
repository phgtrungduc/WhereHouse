using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLUser : BLBase<User>,IBLUser
    {
        IDLUser _dlUser;
        public BLUser(IDLUser dLUser):base(dLUser) 
        {
            _dlUser = dLUser;
        }
        public override bool BeforeInsert(ref User entity)
        {
            var res = false;
            if (!string.IsNullOrEmpty(entity.Password)) {
                LoginParam loginParam = new LoginParam();
                loginParam = PasswordHash.HashPassword(entity.Password);
                if (loginParam != null) {
                    entity.Password = loginParam.HashPassword;
                    entity.Salt = loginParam.Salt;
                    res =  true;
                }
            }
            return res;
        }
    }
}
