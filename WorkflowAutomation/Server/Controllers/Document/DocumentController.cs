using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Reflection;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;


using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using WorkflowAutomation.Application.Documents.Queries.GetDocumentList;
using WorkflowAutomation.Application.Documents.Commands.DeleteDocument;
using WorkflowAutomation.Application.Documents.Queries.GetAllowedDocumentList;
using WorkflowAutomation.Application.Documents.Queries.GetDocument;

namespace WorkflowAutomation.Server.Controllers
{
    //[ApiVersion("1.0")]
    //[Produces("application/json")]
    //[Route("api/{version:apiVersion}/[controller]")]
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : BaseController
    {
        //    private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IWebHostEnvironment env, IMapper mapper, ILogger<DocumentController> logger)
        {
            _env = env;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("CreateNewDocument")]
        [Authorize]
        public async Task<ActionResult<int>> CreateNewDocument([FromForm]CreateNewDocumentDto createNewDocumentDto)
        {
        var command = _mapper.Map<CreateNewDocumentCommand>(createNewDocumentDto);
            command.UserId = UserId.ToString();
            command.resourcePath = new Uri($"{Request.Scheme}://{Request.Host}/");
            command.Files = createNewDocumentDto.FilesToUpload;
            command.ContentRootPath = _env.ContentRootPath;
            command.EnvironmentName = _env.EnvironmentName;
            //TODO: вынести в appsettings
            command.MaxAllowedFiles = 3;
            command.MaxFileSize = 15 * 1024 * 1024;
            var docId = await Mediator.Send(command);
            
            return Ok(docId);
        }

        [HttpGet("GetAllowedDocuments")]
        [Authorize]
        public async Task<ActionResult<DocumentListVm>> GetAllowedDocuments()
        {
            var query = new GetAllowedDocumentListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("GetAllDocuments")]
        [Authorize]
        public async Task<ActionResult<DocumentListVm>> GetAllDocuments()
        {
            var query = new GetDocumentListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("GetDocument/{id}")]
        [Authorize]
        public async Task<ActionResult<GetDocumentDto>> GetDocument(int id)
        {
            var query = new GetDocumentQuery
            {
                DocumentId = id,
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteUpdate(int id, [FromBody] DeleteDocumentDto deleteDocumentDto)
        {
            var command = _mapper.Map<DeleteDocumentCommand>(deleteDocumentDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

    }
}
