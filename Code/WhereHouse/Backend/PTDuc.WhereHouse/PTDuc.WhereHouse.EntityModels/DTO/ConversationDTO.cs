using PTDuc.WhereHouse.DBContext.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.EntityModels.DTO
{
    public partial class ConversationDTO
    {

        public Guid ConversationId { get; set; }
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
