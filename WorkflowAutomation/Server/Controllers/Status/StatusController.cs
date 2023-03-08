using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using WorkflowAutomation.Application.Statuses.Commands.ChangeStatus;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatusController : BaseController
    {
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ILogger<StatusController> _logger;
        private HubConnection? _hubConnection;

        public StatusController(IWebHostEnvironment env, IMapper mapper, ILogger<StatusController> logger)
        {
            _env = env;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPut("ChangeStatus")]
        [Authorize]
        public async Task<ActionResult> ChangeStatus([FromBody] ChangeStatusDto changeStatusDto)
        {
            var command = _mapper.Map<ChangeStatusCommand>(changeStatusDto);
            command.UserId = UserId;
            var adress = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/notificationHub/";
            command.Uri = new Uri(adress, UriKind.Absolute);
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
