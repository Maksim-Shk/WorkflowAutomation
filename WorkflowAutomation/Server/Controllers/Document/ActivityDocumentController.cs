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
using WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments;
using WorkflowAutomation.Application.Documents.Queries.GetDocumentList;

namespace WorkflowAutomation.Server.Controllers.Document
{
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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<RecentActivityDocumentListVm>> GetAllowedDocuments()
        {
            var query = new GetRecentActivityDocumentsQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
