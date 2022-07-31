using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using PTDuc.WhereHouse.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.ChatHub
{
    public class ChattingHub : Hub
    {
        List<string> ConnectedUsers;
        IBLUser _bLUser;
        //IBLConversation bLConversation;
        //IBLMessage _bLMessage;
        public ChattingHub(IBLUser bLUser) {
            ConnectedUsers = new List<string>();
            _bLUser = bLUser;
        }

        public async Task UserOnConnected(string userId)
        {
            var user = _bLUser.GetByID(userId);
            
            if (user != null) {
                user.FullName = "Phương Trung Đức";
                _bLUser.Update(user, "123");

                _bLUser.AddUserToListOnline(user);
                //user sẽ được add vào room có id chính là id của user
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SendPrivateMessage(string userReceivedId, string userSendId,string message)
        {
            var groupName = userReceivedId;
            //Vì user đã được thêm vào room là id của chính nó nên user gửi tin nhắn riêng tư cũng sẽ được add vào room này
            await Clients.Group(groupName).SendAsync("ReceivedPrivateMessage", userSendId, message);
        }
    }
}
