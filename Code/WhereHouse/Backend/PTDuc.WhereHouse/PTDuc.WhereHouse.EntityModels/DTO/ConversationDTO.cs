using PTDuc.WhereHouse.DBContext.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public partial class ConversationDTO : BaseEntity
    {

        public Guid ConversationId { get; set; }
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
        public DateTime LastTimeMessage { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual User UserId1Navigation { get; set; }
        public virtual User UserId2Navigation { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string AvatarUrl { get; set; }
    }
}
