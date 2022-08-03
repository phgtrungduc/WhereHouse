using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.BL.Interfaces
{
    public interface IBLConversation : IBLBase<Conversation, ConversationDTO>
    {
        Conversation GetConversation(string userId1, string userId2);
    }
}
