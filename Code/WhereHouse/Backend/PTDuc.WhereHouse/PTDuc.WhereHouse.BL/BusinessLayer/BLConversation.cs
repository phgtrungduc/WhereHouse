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
using System.Linq;

namespace PTDuc.WhereHouse.BL.BusinessLayer
{
    public class BLConversation : BLBase<Conversation, ConversationDTO>, IBLConversation
    {
        IDLConversation _DLConversation;
        public BLConversation(IDLConversation DLConversation, IMapper mapper) : base(DLConversation, mapper)
        {
            _DLConversation = DLConversation;
        }

        public List<ConversationDTO> GetAllConversationUser(string userId)
        {
            var data = _DLConversation.GetAllConversationUser(userId);
            var dataDTO = _mapper.Map<List<ConversationDTO>>(data);
            dataDTO.ForEach(x=> {
                if (x.UserId1.ToString() == userId)
                {
                    x.UserId = x.UserId2;
                    x.FullName = x.UserId2Navigation.FullName;
                    x.UserName = x.UserId2Navigation.UserName;
                    if (x.UserId2Navigation.Avatar != null)
                    {
                        x.AvatarUrl = x.UserId2Navigation.Avatar.FilePath;
                    }
                }
                else {
                    x.UserId = x.UserId1;
                    x.FullName = x.UserId1Navigation.FullName;
                    x.UserName = x.UserId1Navigation.UserName;
                    if (x.UserId1Navigation.Avatar != null)
                    {
                        x.AvatarUrl = x.UserId1Navigation.Avatar.FilePath;
                    }
                }
            });
            return dataDTO.OrderByDescending(x=>x.LastTimeMessage)?.ToList();
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
                    UserId2 = Guid.Parse(userRecievedId),
                    LastTimeMessage = DateTime.Now
                }) ;
                return newid;
            }
        }
    }
}
