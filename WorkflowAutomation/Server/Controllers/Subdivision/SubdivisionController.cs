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

using Microsoft.EntityFrameworkCore;
using System.Threading;
using WorkflowAutomation.Domain;
using System.Reflection;
using WorkflowAutomation.Application.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using WorkflowAutomation.Application.Documents.Queries.GetSubdivisionList;
using WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo;
using MediatR;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SubdivisionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<SubdivisionController> _logger;
        private readonly IDocumentUserDbContext _dbContext;

        public SubdivisionController(
           IMapper mapper,
            ILogger<SubdivisionController> logger, IDocumentUserDbContext dbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<SubdivisionListLookupDto>>> SubdivisionGet()
        {
            var query = new GetSubdivisionListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("GetSubdivisionInfo/{id}")]
        [Authorize]
        public async Task<ActionResult<SubdivisionInfoDto>> GetSubdivisionInfo(int id)
        {
            var query = new GetSubdivisionInfoQuery
            {
                UserId = UserId,
                SubdivisionId = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
