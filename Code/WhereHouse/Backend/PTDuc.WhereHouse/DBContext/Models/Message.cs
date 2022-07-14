using System;
using System.Collections.Generic;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class Message
    {
        public Guid MessageId { get; set; }
        public Guid? ConversationId { get; set; }
        public Guid UserId { get; set; }
        public DateTime Time { get; set; }
        public string Content { get; set; }

        public virtual Conversation Conversation { get; set; }
        public virtual User User { get; set; }
    }
}
