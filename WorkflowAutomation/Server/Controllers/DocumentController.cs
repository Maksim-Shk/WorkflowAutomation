using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using WorkflowAutomation.Application.Documents.Queries.GetDocumentList;
using WorkflowAutomation.Application.Documents.Commands.UserInfoCommand;
using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Server.Controllers;
using WorkflowAutomation.Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WorkflowAutomation.Domain;

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
        public async Task<ActionResult<int>> CreateNewDocument([FromBody] CreateNewDocumentDto createDocumentDto)
        {
            //Task<ActionResult<Guid> 
            var command = _mapper.Map<CreateNewDocumentCommand>(createDocumentDto);
            command.UserId = UserId.ToString();
            var documentId = await Mediator.Send(command);
            return Ok(documentId);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<SimpleDocumentListVm>> GetSimpleDocuments()
        {
            var query = new GetSimpleDocumentListQuery
            {
                UserId = UserId.ToString()
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<SimpleDocumentListVm>> CreateUserInfo([FromBody] CreateUserInfoDto createUserInfoDto)
        {
            var command = _mapper.Map<CreateUserInfoCommand>(createUserInfoDto);
            command.UserId = UserId;
            var userId = await Mediator.Send(command);
            return Ok(userId);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<SubdivisionListLookupDto>> GetAllSubdivisions()
        {
            var query = new GetSubdivisionListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

      //[HttpGet]
      //[Authorize]
      //public async Task<ActionResult<SubdivisionListLookupDto>> GetAllPositions()
      //{
      //    
      //
      //}
    }
}
