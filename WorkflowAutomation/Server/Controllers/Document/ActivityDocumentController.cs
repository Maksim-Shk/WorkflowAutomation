using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments;

namespace WorkflowAutomation.Server.Controllers.Document;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ActivityDocumentController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ILogger<ActivityDocumentController> _logger;

    public ActivityDocumentController(IMapper mapper, ILogger<ActivityDocumentController> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet("RecentActivityDocument")]
    [Authorize]
    public async Task<ActionResult<RecentActivityDocumentListVm>> GetRecentActivityDocument()
    {
        var query = new GetRecentActivityDocumentsQuery
        {
            UserId = UserId,
            NumberOfEntity = 10
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
}
