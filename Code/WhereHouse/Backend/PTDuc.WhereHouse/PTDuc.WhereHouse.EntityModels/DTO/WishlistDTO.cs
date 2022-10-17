using PTDuc.WhereHouse.DBContext.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class WishlistDTO:BaseEntity
    {
        public Guid WishlistId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public virtual PostDTO Post { get; set; }

        public string HouseImageUrl { get; set; }
    }
}
