using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery;
using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Server.Controllers;
using WorkflowAutomation.Shared;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WorkflowAutomation.Domain;
using System.Reflection;
using WorkflowAutomation.Application.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using MediatR;

namespace WorkflowAutomation.Server.Controllers
{
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
}
