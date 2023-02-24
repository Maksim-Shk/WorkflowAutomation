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
        private HubConnection? _hubConnection;
        public SignalController(
           IMapper mapper,
           ILogger<SignalController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        //[HttpGet("Test/{path}")]
        [HttpGet]
        [Authorize]
        public async Task/*<ActionResult<string>>*/ Test(/*string path*/)
        {
            //TODO использовать абсолютный путь
            Uri myUri = new Uri("https://localhost:7225/chathub/", UriKind.Absolute);
            _hubConnection = new HubConnectionBuilder()
         .WithUrl(myUri)
         .Build();
            await _hubConnection.StartAsync();
            await _hubConnection.SendAsync("Send", "пользователь", "сообщение пользователя");

            await _hubConnection.DisposeAsync();
            //return path;
        }
    }
}
