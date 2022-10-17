using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLReport : IDLBase<Report, ReportDTO>
    {
        Report UserHasReported(Guid userId, Guid postId);
    }
}
