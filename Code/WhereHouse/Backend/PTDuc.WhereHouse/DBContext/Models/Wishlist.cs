using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class Wishlist
    {
        public Guid WishlistId { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
