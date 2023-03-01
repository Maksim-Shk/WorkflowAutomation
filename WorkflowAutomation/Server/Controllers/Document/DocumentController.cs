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
using WorkflowAutomation.Application.Documents.Queries.GetOneDocument;
using Microsoft.AspNetCore.SignalR.Client;

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
        private HubConnection? _hubConnection;

        public DocumentController(IWebHostEnvironment env, IMapper mapper, ILogger<DocumentController> logger)
        {
            _env = env;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("CreateNewDocument/{jwtToken}")]
        [Authorize]
        public async Task<ActionResult<int>> CreateNewDocument([FromForm]CreateNewDocumentDto createNewDocumentDto, string jwtToken)
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
            command.jwtToken = jwtToken;
            var adress = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/notificationHub/";
            command.Uri = new Uri(adress, UriKind.Absolute);
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
        public async Task<ActionResult<DocumentDto>> GetDocument(int id)
        {
          //  string workingDirectory = Environment.CurrentDirectory;
            // This will get the current PROJECT bin directory (ie ../bin/)
         //   string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        //    var uploadDirectory = projectDirectory + @"\WorkflowAutomation\Server\Development\unsafe_uploads";
            var query = new GetDocumentQuery
            {
                DocumentId = id,
                UserId = UserId,
              //  DirectoryPath = uploadDirectory
            };
            var dto = await Mediator.Send(query);
            return Ok(dto);
        }
       //[HttpGet("GetDocument/{id}")]
       //[Authorize]
       //public async Task<ActionResult<AnotherGetDocumentDto>> GetDocument(int id)
       //{
       //    var query = new AnotherGetDocumentQuery
       //    {
       //        DocumentId = id,
       //        UserId = UserId
       //    };
       //    var vm = await Mediator.Send(query);
       //    return Ok(vm);
       //}

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
