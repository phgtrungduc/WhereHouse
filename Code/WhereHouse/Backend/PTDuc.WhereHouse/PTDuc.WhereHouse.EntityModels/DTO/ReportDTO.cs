using PTDuc.WhereHouse.DBContext.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class ReportDTO : BaseEntity
    {
        public Guid ReportId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserReportId { get; set; }
        public byte Status { get; set; }

        public virtual Post Post { get; set; }
        public virtual User UserReport { get; set; }
    }
}
