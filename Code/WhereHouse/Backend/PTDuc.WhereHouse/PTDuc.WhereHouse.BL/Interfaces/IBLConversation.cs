using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLConversation : IBLBase<Conversation, ConversationDTO>
    {
        Conversation GetConversation(string userId1, string userId2);
        List<Conversation> GetAllConversationUser(string userId);

        Guid InitChat(string userRecievedId, string userSendId);  
    }
}
