using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLReport : BLBase<Report, ReportDTO>, IBLReport
    {
        IDLReport _DLReport;
        public BLReport(IDLReport DLReport, IMapper mapper) : base(DLReport, mapper)
        {
            _DLReport = DLReport;
        }
    }
}
