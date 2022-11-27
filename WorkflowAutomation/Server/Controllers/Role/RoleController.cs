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

using WorkflowAutomation.Shared;
using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Application.Roles.Queries.GetRolesList;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoleController : BaseController
    {
        private readonly IMapper _mapper;

        public RoleController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<RolesListVm>> SubdivisionGet()
        {
            var query = new GetRolesListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}