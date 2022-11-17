using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using WorkflowAutomation.Application.Documents.Queries.GetDocumentList;
using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Server.Controllers;
using WorkflowAutomation.Shared;

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
        private readonly IMapper _mapper;

        public DocumentController(IMapper mapper) => _mapper = mapper;

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> CreateNewDocument([FromBody] CreateNewDocumentDto createDocumentDto)
        {
            //Task<ActionResult<Guid> 
            var command = _mapper.Map<CreateNewDocumentCommand>(createDocumentDto);
            command.UserId = UserId;
            var documentId = await Mediator.Send(command);
            return Ok(documentId);
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<SimpleDocumentListVm>> GetSimpleDocuments()
        {
            var query = new GetSimpleDocumentListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

    }
}
