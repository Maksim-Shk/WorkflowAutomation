/*
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Web;
public class CustomUserIdProvider : IUserIdProvider
{
    public virtual string GetUserId(HubConnectionContext connection)
    {
        // return int.Parse(this.User.Claims.First(i => i.Type == "UserId").Value);
        //var UserId = connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        return "949d9ee0-6b92-445d-aa1a-9d8e9c7d1f51";
        //return connection.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
*/