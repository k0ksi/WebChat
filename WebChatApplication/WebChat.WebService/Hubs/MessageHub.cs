using System;
using Microsoft.AspNet.SignalR;

namespace WebChat.WebService.Hubs
{
    public class MessageHub : Hub
    {
        public void SendMessageToChatroom(Guid chatroomId, string name, string message)
        {
            this.Clients.Group(chatroomId.ToString()).broadcastMessage(name, message);
        }

        public void SendMessageToUser(string userId, string name, string message)
        {
            this.Clients.User(userId).broadcastMessage(name, message);
        }

        public void JoinChatroom(Guid id)
        {
            this.Groups.Add(this.Context.ConnectionId, id.ToString());
        }

        public void LeaveChatroom(Guid id)
        {
            this.Groups.Remove(this.Context.ConnectionId, id.ToString());
        }
    }
}