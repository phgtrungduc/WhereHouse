using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class HouseType
    {
        public HouseType()
        {
            Houses = new HashSet<House>();
        }

        public Guid HouseTypeId { get; set; }
        public string HouseTypeName { get; set; }

        public virtual ICollection<House> Houses { get; set; }
    }
}
