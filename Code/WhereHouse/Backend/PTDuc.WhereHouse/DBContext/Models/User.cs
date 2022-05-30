using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class User
    {
        public User()
        {
            Houses = new HashSet<House>();
            Posts = new HashSet<Post>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public decimal? Amount { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int Role { get; set; }

        public virtual ICollection<House> Houses { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
