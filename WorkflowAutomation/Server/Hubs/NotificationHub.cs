using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WorkflowAutomation.Server.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificationHub : Hub
    {
        public async Task Send(string user, string message)
        {
            await this.Clients.All.SendAsync("Receive", user, message) ;
        }
        public async Task SendNotification(string userId, string Header, string Body)
        {
            var userName = Context.User.Identity.Name;
            await this.Clients.User(userId).SendAsync("ReceiveNotification", Header, Body);
        }
        public async Task SendMessageToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync("ReceiveMessageToUser", message);
        }

    }
}