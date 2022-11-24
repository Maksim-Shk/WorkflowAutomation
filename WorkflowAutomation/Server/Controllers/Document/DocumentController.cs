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
        private readonly IMapper _mapper;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IMapper mapper, ILogger<DocumentController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<int>> CreateNewDocument(CreateNewDocumentDto createNewDocumentDto)
        {
            //Task<ActionResult<Guid> 
            var command = _mapper.Map<CreateNewDocumentCommand>(createNewDocumentDto);
            command.UserId = UserId.ToString();
            var docId = await Mediator.Send(command);

            return Ok(docId);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<DocumentListVm>> GetAllowedDocuments()
        {
            var query = new GetDocumentListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        //
        //   [HttpPost]
        //   [Authorize]
        //   public async Task<ActionResult<int>> CreateNewDocument([FromBody] CreateNewDocumentDto createDocumentDto)
        //   {
        //       //Task<ActionResult<Guid> 
        //       var command = _mapper.Map<CreateNewDocumentCommand>(createDocumentDto);
        //       command.UserId = UserId.ToString();
        //       var documentId = await Mediator.Send(command);
        //       return Ok(documentId);
        //   }
        //
        //   //[HttpGet]
        //   //[Authorize]
        //   //public async Task<ActionResult<SimpleDocumentListVm>> GetSimpleDocuments()
        //   //{
        //   //    var query = new GetSimpleDocumentListQuery
        //   //    {
        //   //        UserId = UserId.ToString()
        //   //    };
        //   //    var vm = await Mediator.Send(query);
        //   //    return Ok(vm);
        //   //}
        //
        //   [HttpPut]
        //   [Authorize]
        //   public async Task<ActionResult<SimpleDocumentListVm>> CreateUserInfo([FromBody] CreateUserInfoDto createUserInfoDto)
        //   {
        //       var command = _mapper.Map<CreateUserInfoCommand>(createUserInfoDto);
        //       command.UserId = UserId;
        //       var userId = await Mediator.Send(command);
        //       return Ok(userId);
        //   }

        //  [HttpGet]
        //  [Authorize]
        //  public async Task<ActionResult<List<Subdivision>>> GetAllSubdivisions()
        //  {
        //      //List<SubdivisionListLookupDto> SubdivisionListLookupDtos = new List<SubdivisionListLookupDto>{
        //      //    new SubdivisionListLookupDto { Id = 1, IdSubordination = 1, Name = "1111" },
        //      //    new SubdivisionListLookupDto { Id = 2, IdSubordination = 1, Name = "22" },
        //      //    new SubdivisionListLookupDto { Id = 3, IdSubordination = 1, Name = "33" },
        //      //    new SubdivisionListLookupDto { Id = 4, IdSubordination = 1, Name = "44" }
        //      //};
        //
        //      var SubdivisionListLookupDtos2 = await _dbContext.Subdivisions
        //          // .ProjectTo<SubdivisionListLookupDto>(_mapper.ConfigurationProvider)
        //          .ToListAsync();
        //      _logger.LogError(SubdivisionListLookupDtos2.Count.ToString() + " !!!!!))))");
        //      return SubdivisionListLookupDtos2;
        //      //new SubdivisionListVm { Subdivisions = subdivisionsQuery };
        //      //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //      //_logger.LogError("Количество сборок = " + assemblies.Length.ToString());
        //      //var query = new GetSubdivisionListQuery
        //      //{
        //      //    UserId = UserId
        //      //};
        //      //var vm = await Mediator.Send(query);
        //      //return Ok(vm);
        //
        //
        //  }

        //[HttpGet]
        //[Authorize]
        //public async Task<ActionResult<SubdivisionListLookupDto>> GetAllPositions()
        //{
        //    
        //
        //}
    }
}
