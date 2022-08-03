using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLConversation : IDLBase<Conversation, ConversationDTO>
    {
        Conversation GetConversation(string userId1, string userId2);
    }
}
