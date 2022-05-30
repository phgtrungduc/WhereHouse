using PTDuc.WhereHouse.BL.BusinessLayer;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace PTDuc.WhereHouse.Controllers
{
    public class UserController
        : BaseEntityController<User>
    {
        IBLUser _blUser;
        public UserController(IBLUser blUser) : base(blUser)
        {
            _blUser = blUser;
        }
    }
}
