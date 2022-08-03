using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class FileDTO : BaseEntity
    {
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
