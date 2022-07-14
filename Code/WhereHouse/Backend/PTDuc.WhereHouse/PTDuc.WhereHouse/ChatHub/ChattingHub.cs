using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PTDuc.WhereHouse.ChatHub
{
    public class ChattingHub : Hub
    {
        List<string> ConnectedUsers;
        public ChattingHub() {
            ConnectedUsers = new List<string>(); 
        }

        public async Task Connect(string name)
        {
            { } ConnectedUsers.Add(name);
           var a =  Context.UserIdentifier;
            await Clients.All.SendAsync("Connected","PTDuc");
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
        public async Task SendMessageDuc(string userId,string mess)
        {

            await Clients.Users(userId).SendAsync("Send",mess);
        }

        public async Task SendMessageToGroup(string groupName,string mess)
        {
            await Clients.Group(groupName).SendAsync("SendMessageToGroup", $"{Context.ConnectionId} has joined the group {groupName}.");
        }

        public async Task SendPrivateMessage(string groupName, string userSendId,string message)
        {
            //await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("ReceivedPrivateMessage", userSendId, message);
        }
    }
}
