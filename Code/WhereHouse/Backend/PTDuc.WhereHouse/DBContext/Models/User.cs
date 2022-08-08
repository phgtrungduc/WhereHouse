using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class User
    {
        public User()
        {
            ConversationUserId1Navigations = new HashSet<Conversation>();
            ConversationUserId2Navigations = new HashSet<Conversation>();
            Houses = new HashSet<House>();
            Messages = new HashSet<Message>();
            Posts = new HashSet<Post>();
            Reports = new HashSet<Report>();
            Wishlists = new HashSet<Wishlist>();
        }

        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Guid? AvatarId { get; set; }
        public int Role { get; set; }
        public byte Status { get; set; }
        public bool? IsOnline { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        public virtual File Avatar { get; set; }
        public virtual ICollection<Conversation> ConversationUserId1Navigations { get; set; }
        public virtual ICollection<Conversation> ConversationUserId2Navigations { get; set; }
        public virtual ICollection<House> Houses { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
