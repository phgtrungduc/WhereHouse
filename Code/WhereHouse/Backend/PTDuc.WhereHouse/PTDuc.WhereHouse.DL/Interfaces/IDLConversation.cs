using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLConversation : IDLBase<Conversation, ConversationDTO>
    {
        Conversation GetConversation(string userId1, string userId2);
        List<Conversation> GetAllConversationUser(string userId);
    }
}
