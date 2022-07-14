using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.Controllers
{
    public class HouseController : BaseEntityController<House, HouseDTO>
    {
        IBLHouse _blHouse;
        public HouseController(IBLHouse blHouse) : base(blHouse)
        {
            _blHouse = blHouse;
        }

        [AllowAnonymous]
        public override IActionResult Get()
        {
            return base.Get();
        }

        [AllowAnonymous]
        public override IActionResult GetByPaging(int page, int pageSize)
        {
            return base.GetByPaging(page, pageSize);
        }

        [AllowAnonymous]
        public override IActionResult GetByID(string id)
        {
            return base.GetByID(id);
        }

        [HttpGet("GetDeepData")]
        public virtual IActionResult GetDeepData()
        {
            return Ok(_blHouse.GetDeepData());
        }

        [HttpGet("GetHouseDetail")]
        public virtual IActionResult GetHouseDetail(string houseId)
        {
            var res = new ServiceResult();

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                res.Data = null; 
            }

            return Ok(res);
        }
    }
}
