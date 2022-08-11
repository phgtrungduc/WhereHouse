using Microsoft.AspNetCore.Mvc;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.Controllers
{
    public class ReportController : BaseEntityController<Report, ReportDTO>
    {
        IBLReport _blReport;
        public ReportController(IBLReport blReport) : base(blReport)
        {
            _blReport = blReport;
        }
        public override IActionResult Post([FromBody] ReportDTO entity)
        {
            return base.Post(entity);
        }

    }
}
