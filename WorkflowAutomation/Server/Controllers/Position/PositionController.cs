using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Documents.Queries.GetPositionList;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PositionController : BaseController
    {
        private readonly ILogger<PositionController> _logger;

        public PositionController(
           ILogger<PositionController> logger)
        {
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
    }
}
