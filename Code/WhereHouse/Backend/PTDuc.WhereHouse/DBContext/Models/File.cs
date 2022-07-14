using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class File
    {
        public File()
        {
            Houses = new HashSet<House>();
            Users = new HashSet<User>();
        }

        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual ICollection<House> Houses { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
