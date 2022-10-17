using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace PTDuc.WhereHouse.DL.DatabaseLayer
{
    public class DLReport : DLBase<Report, ReportDTO>, IDLReport
    {
        public DLReport(WhereHouseContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public Report UserHasReported(Guid userId, Guid postId)
        {
            _dbSet = _context.Set<Report>();
            var data = _dbSet.Where(report => report.UserReportId == userId && report.PostId == postId).FirstOrDefault();
            return data;
        }
        public override IEnumerable<ReportDTO> GetAll()
        {
            _dbSet = _context.Set<Report>();
            var data = _dbSet.Include(x => x.UserReport).Include(x => x.Post)?.ToList();
            var dataDTO = _mapper.Map<List<ReportDTO>>(data);
            dataDTO.ForEach(x=> {
                x.FullName = x.UserReport.FullName;
                x.UserName = x.UserReport.UserName;
            });
            return dataDTO;
        }
    }
}
