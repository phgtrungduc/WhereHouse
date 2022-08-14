using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.Controllers
{
    public class ReportController : BaseEntityController<Report, ReportDTO>
    {
        IBLReport _blReport;
        IBLUser _bLUser;
        public ReportController(IBLReport blReport, IBLUser bLUser) : base(blReport)
        {
            _blReport = blReport;
            _bLUser = bLUser;
        }
        public override IActionResult Get()
        {
            var res = new ServiceResult();
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                var userId = User.Claims.Where(c => c.Type == "UserId")
    .Select(x => x.Value).FirstOrDefault();
                var user = _bLUser.GetByID(userId);
                if (user != null && user.Role == (int)Enumeration.Role.Admin)
                {
                    res.Data = _blReport.GetAll();
                }
                else
                {
                    res.StatusCode = (int)Enumeration.ResultCode.NotHaveRight;
                    res.Data = false;
                }
                return this.HandleResponse(res);
            }
            return BadRequest(new ServiceResult() { Data = false });
        }

    }
}
