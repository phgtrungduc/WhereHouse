using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class HouseDTO
    {

        public Guid HouseId { get; set; }
        public Guid UserOwnerId { get; set; }
        public string HouseName { get; set; }
        public Guid? HouseTypeId { get; set; }
        public float? Area { get; set; }
        public int NumberOfBedroom { get; set; }
        public int? TotalOfFloor { get; set; }
        public float? Horizontal { get; set; }
        public float? Vertical { get; set; }
        public decimal? Price { get; set; }
        public Guid? HouseImageId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public bool? IsInWishList { get; set; }
    }
}
