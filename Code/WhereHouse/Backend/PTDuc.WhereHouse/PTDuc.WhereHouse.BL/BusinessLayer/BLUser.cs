using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLUser : BLBase<User>,IBLUser
    {
        IDLUser _dlUser;
        public BLUser(IDLUser dLUser):base(dLUser) 
        {
            _dlUser = dLUser;
        }
    }
}
