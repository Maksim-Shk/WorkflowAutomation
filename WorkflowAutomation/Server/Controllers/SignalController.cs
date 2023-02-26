using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Server.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SignalController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SignalController> _logger;
        private HubConnection? _hubConnection;
        public SignalController(
           IMapper mapper,
           ILogger<SignalController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpGet("Test/{path}")]
        [HttpGet("GetRecive/{jwtToken}")]
        [Authorize]
        public async Task/*<ActionResult<string>>*/ Test(string jwtToken)
        {
            //TODO использовать абсолютный путь
            Uri myUri = new Uri("https://localhost:7225/notificationHub/", UriKind.Absolute);
            _hubConnection = new HubConnectionBuilder()
         .WithUrl(myUri)
         .Build();
            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("Send", "пользователь", "сообщение пользовател€");

            await _hubConnection.DisposeAsync();
            //return path;
        }
        [HttpGet("GetReciveUser/{jwtToken}")]
        [Authorize]
        public async Task GetReciveUser(string jwtToken)
        {
            Uri myUri = new Uri("https://localhost:7225/notificationHub/", UriKind.Absolute);
            _hubConnection = new HubConnectionBuilder()
         .WithUrl(myUri, options =>
         {
             options.AccessTokenProvider = () => Task.FromResult(jwtToken);
         }).WithAutomaticReconnect()
           .Build();
            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("SendNotification", UserId, "«аголовок", "“ело");

            await _hubConnection.DisposeAsync();
            //return path;
        }
    }
}
