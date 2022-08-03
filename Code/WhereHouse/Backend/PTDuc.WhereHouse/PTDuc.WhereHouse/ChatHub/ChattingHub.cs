using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.ChatHub
{
    public class ChattingHub : Hub
    {
        IBLUser _bLUser;
        IBLConversation _bLConversation;
        //IBLConversation bLConversation;
        //IBLMessage _bLMessage;
        public ChattingHub(IBLUser bLUser, IBLConversation bLConversation)
        {
            _bLUser = bLUser;
            _bLConversation = bLConversation;
        }

        public async Task UserOnConnected(string userId)
        {
            var user = _bLUser.GetByID(userId);
            if (user != null)
            {
                //user sẽ được add vào room có id chính là id của user
                await this.AddToGroup(userId);
            }
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendPrivateMessage(string userReceivedId, string userSendId, string message)
        {
            var groupName = userReceivedId;
            
            //Vì user đã được thêm vào room là id của chính nó nên user gửi tin nhắn riêng tư cũng sẽ được add vào room này
            await Clients.Group(groupName).SendAsync("ReceivedPrivateMessage", userSendId, message);
        }

        public async Task InitPrivateChat(string userSendId,string userReceivedId)
        {
            var conversation = _bLConversation.GetConversation(userSendId, userReceivedId);
            if (conversation != null)
            {
                await Clients.Group(userReceivedId).SendAsync("InitPrivateRoomChat", conversation.ConversationId);
                await Clients.Group(userSendId).SendAsync("InitPrivateRoomChat", conversation.ConversationId);
            }
            else
            {
                var conversationId = Guid.NewGuid();
                var newConversation = new ConversationDTO()
                {
                    ConversationId = conversationId,
                    UserId1 = Guid.Parse(userSendId),
                    UserId2 = Guid.Parse(userReceivedId)
                };
                _bLConversation.Insert(newConversation);
                await Clients.Group(userReceivedId).SendAsync("InitPrivateRoomChat", conversationId.ToString());
                await Clients.Group(userSendId).SendAsync("InitPrivateRoomChat", conversationId.ToString());
            }
        }
    }
}
