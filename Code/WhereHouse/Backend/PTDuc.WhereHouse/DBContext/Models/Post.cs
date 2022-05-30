using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class Post
    {
        public Guid PostId { get; set; }
        public string Title { get; set; }
        public string Descrtiption { get; set; }
        public Guid UserId { get; set; }
        public Guid HouseId { get; set; }

        public virtual House House { get; set; }
        public virtual User User { get; set; }
    }
}
