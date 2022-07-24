using PTDuc.WhereHouse.BL.BusinessLayer;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.EntityModels.DTO;

namespace PTDuc.WhereHouse.Controllers
{
    public class UserController
        : BaseEntityController<User, UserDTO>
    {
        IBLUser _blUser;
        public UserController(IBLUser blUser) : base(blUser)
        {
            _blUser = blUser;
        }
        [AllowAnonymous]
        public override IActionResult Post([FromBody] UserDTO entity)
        {
            return base.Post(entity);
        }
    }
}
