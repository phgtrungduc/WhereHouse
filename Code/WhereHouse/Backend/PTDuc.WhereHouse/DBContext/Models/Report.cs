using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class Report
    {
        public Guid ReportId { get; set; }
        public Guid PostId { get; set; }
        public Guid UserReportId { get; set; }
        public string Content { get; set; }
        public byte Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual Post Post { get; set; }
        public virtual User UserReport { get; set; }
    }
}
