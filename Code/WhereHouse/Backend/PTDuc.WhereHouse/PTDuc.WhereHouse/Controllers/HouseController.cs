using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;

namespace PTDuc.WhereHouse.Controllers
{
    public class HouseController : BaseEntityController<House>
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

        [HttpGet("GetDeepData")]
        public virtual IActionResult GetDeepData()
        {
            return Ok(_blHouse.GetDeepData());
        }
    }
}
