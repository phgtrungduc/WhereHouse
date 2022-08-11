using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class HouseDTO : BaseEntity
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

        public bool? IsInWishList { get; set; }

        public string Title { get; set; }

        public string Descrtiption { get; set; }
        public Guid? UserId { get; set; }

        public string Address { get; set; }
        public string AddressByGoogle { get; set; }
        public Guid? PostId { get; set; }
    }
}
