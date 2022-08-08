using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLMessage : IBLBase<Message, MessageDTO>
    {
        object GetConversation(MessageDTO message);
        List<Message> GetMessagesByConversationId(string conversationId);
    }
}
