using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Positions.Commands.CreateNewPositionCommand;
using WorkflowAutomation.Application.Positions.Queries.GetPositionList;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PositionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PositionController> _logger;

        public PositionController(
           IMapper mapper,
           ILogger<PositionController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<PositionListVm>>> PositionsGet()
        {
            var query = new GetPositionListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Администратор")]
        public async Task<ActionResult<int>> CreateNewPosition([FromForm] CreatePositionDto createNewDocumentDto)
        {
            var command = _mapper.Map<CreatePositionCommand>(createNewDocumentDto);
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }
    }
}
