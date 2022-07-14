using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class FileDTO
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
