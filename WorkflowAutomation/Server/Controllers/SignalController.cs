using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;


namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SignalController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SignalController> _logger;
        private HubConnection? hubConnection;
        public SignalController(
           IMapper mapper,
            ILogger<SignalController> logger, IDocumentUserDbContext dbContext)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<string>> Test(string user, string message)
        {    
            List<string> messages = new List<string>();
            hubConnection = new HubConnectionBuilder()
            .WithUrl("/chathub")
           .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                messages.Add(encodedMsg);
            });

            await hubConnection.StartAsync();

            if (hubConnection is not null)
            {
               // await hubConnection.SendAsync("SendMessage", userInput, messageInput);
            }
            return "test";
        }
       
    }
}
