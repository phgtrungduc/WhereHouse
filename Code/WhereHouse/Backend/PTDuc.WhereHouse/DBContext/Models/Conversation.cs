using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class Conversation
    {
        public Conversation()
        {
            Messages = new HashSet<Message>();
        }

        public Guid ConversationId { get; set; }
        public Guid UserId1 { get; set; }
        public Guid UserId2 { get; set; }

        public virtual User UserId1Navigation { get; set; }
        public virtual User UserId2Navigation { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
