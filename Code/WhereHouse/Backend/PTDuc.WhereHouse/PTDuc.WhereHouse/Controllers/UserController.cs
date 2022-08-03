using PTDuc.WhereHouse.BL.BusinessLayer;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.BL;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

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
        [HttpPost("InsertAdmin")]
        public IActionResult InsertAdmin([FromBody] UserDTO user)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.InsertAdmin(user, userId);
                return (this.HandleResponse(res));
            }
            return Ok(null);
        }

        [HttpPost("BlockUser")]
        public IActionResult BlockUser([FromBody] string blockUserId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var adminId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.BlockUser(blockUserId, adminId);
                return this.HandleResponse(res);
            }
            return Ok(null);
        }

        [HttpGet("GetListUserForAdmin")]
        public IActionResult GetListUserForAdmin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var adminId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.GetListUserForAdmin(adminId);
                return this.HandleResponse(res);
            }
            return Ok(null);
        }

        [HttpGet("GetUserConfig")]
        public IActionResult GetUserConfig()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
                    .Select(x => x.Value).FirstOrDefault();
                var res = _blUser.GetByID(userId);
                return Ok(res);
            }
            return Ok(null);
        }
    }
}
