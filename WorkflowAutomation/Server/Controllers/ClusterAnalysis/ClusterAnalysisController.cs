using WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis;
using WorkflowAutomation.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkflowAutomation.Server.Controllers;

[Authorize]
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

    [HttpPost("CalculateClusters")]
    [Authorize]
    public async Task<IActionResult> CalculateClusters([FromBody]StartClusterAnalysisDto dto)
    {
        //var dto = new StartClusterAnalysisDto
        //{
        //      ClusterCount = 2,
        //      //TODO 2,{3-4},5
        //      //StatusesIds = new List<int> { 2,5 }
        //};
        dto.ClusterCount = 4;
       
        var command = _mapper.Map<ClusterAnalysisCommand>(dto);
        var vm = await Mediator.Send(command);
        return Ok(vm);
    }
}
