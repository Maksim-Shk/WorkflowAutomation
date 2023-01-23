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
         //  List<Subdivision> subdivisions = new List<Subdivision>{
         //     new Subdivision { IdSubdivision = 1, IdSubordination = 1, Name = "1111" },
         //     new Subdivision { IdSubdivision = 2, IdSubordination = 1, Name = "22" },
         //     new Subdivision { IdSubdivision = 3, IdSubordination = 1, Name = "33" },
         //     new Subdivision { IdSubdivision = 4, IdSubordination = 1, Name = "44" }
         // };
         //
         //
         //  // List<Subdivision> subdivisions2 = _dbContext.Subdivisions.ToList();
         //  // subdivisions2.Clear();
         //  // subdivisions2 = subdivisions;
         //  List<SubdivisionListLookupDto> subdivisions2 = _dbContext.Subdivisions
         //      .ProjectTo<SubdivisionListLookupDto>(_mapper.ConfigurationProvider)
         //      .ToList();
         //
         //  return subdivisions2;

            var query = new GetSubdivisionListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
