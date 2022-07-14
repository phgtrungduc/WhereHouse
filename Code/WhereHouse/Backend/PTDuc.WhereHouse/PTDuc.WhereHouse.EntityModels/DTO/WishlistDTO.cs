using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public class WishlistDTO
    {
        public Guid WishlistId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CreatedTime { get; set; }
    }
}
