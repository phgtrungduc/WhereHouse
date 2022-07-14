using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Collections.Generic;

namespace PTDuc.WhereHouse.DL.Interfaces
{
    public interface IDLMessage : IDLBase<Message, MessageDTO>
    {
        object GetConversation(MessageDTO message);
    }
}
