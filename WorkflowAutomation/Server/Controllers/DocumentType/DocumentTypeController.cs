using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Server.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class DocumentTypeController : BaseController
{
    private readonly IMapper _mapper;
    private readonly ILogger<DocumentTypeController> _logger;
    private readonly IDocumentUserDbContext _dbContext;

    public DocumentTypeController(
       IMapper mapper,
        ILogger<DocumentTypeController> logger, IDocumentUserDbContext dbContext)
    {
        _mapper = mapper;
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<DocumentTypeListVm>> SubdivisionGet()
    {
        var query = new GetDocumentTypesQuery
        {
            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }
}
