using Microsoft.AspNetCore.SignalR;

namespace WorkflowAutomation.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string user, string message)
        {
            await this.Clients.All.SendAsync("Receive", user, message);
        }

        public async Task SendMessageToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessageToUser", message);
        }
    }
}