using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLReport : DLBase<Report, ReportDTO>, IDLReport
    {
        public DLReport(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public bool IsUserHasReported(Guid userId, Guid postId)
        {
            _dbSet = _context.Set<Report>();
            var data = _dbSet.Where(report => report.UserReportId == userId && report.PostId == postId).FirstOrDefault();
            if (data != null) return true;
            return false;
        }
    }
}
