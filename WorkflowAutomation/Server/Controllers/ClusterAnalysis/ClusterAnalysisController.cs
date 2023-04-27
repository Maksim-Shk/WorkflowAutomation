using WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis;
using WorkflowAutomation.Application.Interfaces;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowAutomation.Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ClusterAnalysisController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ClusterAnalysisController> _logger;
        private readonly IDocumentUserDbContext _dbContext;

        public ClusterAnalysisController(
           IMapper mapper,
            ILogger<ClusterAnalysisController> logger, IDocumentUserDbContext dbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> GenerateClusters()
        {
            var dto = new StartClusterAnalysisDto
            {
                 ClusterCount = 2,
                  StatusesIds = new List<int> { 2,6 }
            };
           
            var command = _mapper.Map<ClusterAnalysisCommand>(dto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
