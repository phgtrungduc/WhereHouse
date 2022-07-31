using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class HouseTypeDTO : BaseEntity
    {
        public Guid HouseTypeId { get; set; }
        public string HouseTypeName { get; set; }
    }
}
