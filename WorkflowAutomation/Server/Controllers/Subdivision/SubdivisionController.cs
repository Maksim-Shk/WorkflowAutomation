using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Documents.Queries.GetSubdivisionList;
using WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo;
using WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;


namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SubdivisionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SubdivisionController> _logger;
        private readonly IDocumentUserDbContext _dbContext;

        public SubdivisionController(
           IMapper mapper,
            ILogger<SubdivisionController> logger, IDocumentUserDbContext dbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<SubdivisionListLookupDto>>> SubdivisionGet()
        {
            var query = new GetSubdivisionListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("GetSubdivisionInfo/{id}")]
        [Authorize]
        public async Task<ActionResult<SubdivisionInfoDto>> GetSubdivisionInfo(int id)
        {
            var query = new GetSubdivisionInfoQuery
            {
                UserId = UserId,
                SubdivisionId = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpPut("UpdateSubdivision")]
        [Authorize]
        public async Task<ActionResult<int>> UpdateSubdivision([FromBody] UpdateSubdivisionDto updateSubdivisionDto)
        {
            var command = _mapper.Map<UpdateSubdivisionCommand>(updateSubdivisionDto);
            command.UserId = UserId;
            var subId = await Mediator.Send(command);
            return Ok(subId);
        }
    }
}
