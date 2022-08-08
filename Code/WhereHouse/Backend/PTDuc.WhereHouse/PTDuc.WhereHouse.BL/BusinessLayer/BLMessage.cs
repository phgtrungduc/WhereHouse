using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using System.Collections.Generic;
using AutoMapper;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLMessage : BLBase<Message, MessageDTO>, IBLMessage
    {
        IDLMessage _DLMessage;
        public BLMessage(IDLMessage DLMessage, IMapper mapper) : base(DLMessage, mapper)
        {
            _DLMessage = DLMessage;
        }

        public object GetConversation(MessageDTO message)
        {
            return _DLMessage.GetConversation(message);
        }

        public List<Message> GetMessagesByConversationId(string conversationId)
        {
            return _DLMessage.GetMessagesByConversationId(conversationId);
        }
    }
}
