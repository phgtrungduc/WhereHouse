using Microsoft.Extensions.DependencyInjection;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.DBContext.Models;
using PTDuc.WhereHouse.DL.DatabaseLayer;
using PTDuc.WhereHouse.DL.Interfaces;
using PTDuc.WhereHouse.EntityModels;
using PTDuc.WhereHouse.Utils;
using PTDuc.WhereHouse.EntityModels.DTO;
using AutoMapper;
using System.Collections.Generic;
using System;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLConversation : BLBase<Conversation, ConversationDTO>, IBLConversation
    {
        IDLConversation _DLConversation;
        public BLConversation(IDLConversation DLConversation, IMapper mapper) : base(DLConversation, mapper)
        {
            _DLConversation = DLConversation;
        }

        public List<Conversation> GetAllConversationUser(string userId)
        {
            return _DLConversation.GetAllConversationUser(userId);
        }

        public Conversation GetConversation(string userId1, string userId2)
        {
            return _DLConversation.GetConversation(userId1, userId2);
        }

        public Guid InitChat(string userRecievedId, string userSendId)
        {
            var conversation = this.GetConversation(userSendId, userRecievedId);
            if (conversation != null) return conversation.ConversationId;
            else
            {
                var newid = Guid.NewGuid();
                this.Insert(new ConversationDTO()
                {
                    ConversationId = newid,
                    UserId1 = Guid.Parse(userSendId),
                    UserId2 = Guid.Parse(userRecievedId)
                });
                return newid;
            }
        }
    }
}
