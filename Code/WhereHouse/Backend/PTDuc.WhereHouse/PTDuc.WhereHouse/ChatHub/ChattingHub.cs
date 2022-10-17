using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using PTDuc.WhereHouse.BL.Interfaces;
using PTDuc.WhereHouse.EntityModels.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.ChatHub
{
    public class ChattingHub : Hub
    {
        IBLUser _bLUser;
        IBLConversation _bLConversation;
        IBLMessage _bLMessage;
        //IBLConversation bLConversation;
        //IBLMessage _bLMessage;
        public ChattingHub(IBLUser bLUser, IBLConversation bLConversation, IBLMessage bLMessage)
        {
            _bLUser = bLUser;
            _bLConversation = bLConversation;
            _bLMessage = bLMessage;
        }

        public async Task UserOnConnected(string userId)
        {
            var user = _bLUser.GetByID(userId);
            if (user != null)
            {
                //user sẽ được add vào room có id chính là id của user
                await this.AddToGroup(userId);
                var allConversation = _bLConversation.GetAllConversationUser(userId);
                //Add user vào tất cả các cuộc trò chuyện đã tham gia  
                allConversation.ForEach(async x =>
                {
                    await this.AddToGroup(x.ConversationId.ToString());
                });
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

        public async Task SendPrivateMessage(string userSendId, string groupName, string message)
        {
            //Vì user đã được thêm vào room là id của chính nó nên user gửi tin nhắn riêng tư cũng sẽ được add vào room này
            await Clients.Group(groupName).SendAsync("ReceivedPrivateMessage", userSendId, message);
            var newMessage = new MessageDTO()
            {
                ConversationId = Guid.Parse(groupName),
                UserId = Guid.Parse(userSendId),
                Content = message,
                Time = DateTime.Now
            };
            _bLMessage.Insert(newMessage);
        }

        public async Task InitPrivateChat(string userSendId, string userReceivedId, string connectionId)
        {
            await Clients.Group(userReceivedId).SendAsync("InitPrivateRoomChat", connectionId);
            await Clients.Group(userSendId).SendAsync("InitPrivateRoomChat", connectionId);
        }
    }
}
