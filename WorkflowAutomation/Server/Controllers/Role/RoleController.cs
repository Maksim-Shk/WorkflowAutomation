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
using WorkflowAutomation.Application.Roles.Queries.GetRolesList;
using WorkflowAutomation.Application.Roles.Commands.SetRoleToUser;
using WorkflowAutomation.Application.Roles.Queries.GetUserRolesList;
using WorkflowAutomation.Application.Roles.Commands.RemoveRoleFromUser;
using Microsoft.AspNetCore.Identity;

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

        [HttpGet("AllRoles")]
        [Authorize]
        public async Task<ActionResult<RolesListVm>> GetRoles()
        {
            var query = new GetRolesListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpGet("UserRoles")]
        [Authorize]
        public async Task<ActionResult<UserRolesListVm>> GetUserRoles()
        {
            var query = new GetUserRolesListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        [HttpPut("UpdateRole")]
        [Authorize]
        public async Task<ActionResult> Update([FromBody] SetRoleToUserCommand setRoleToUserDto)
        {
            var command = _mapper.Map<SetRoleToUserCommand>(setRoleToUserDto);
            //command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }

        //[Authorize(Roles = "admin")]
        [HttpPut("DeleteRole")]
        public async Task<IActionResult> Delete([FromBody] RemoveRoleFromUserDto setRoleToUserDto)
        {
            var command = _mapper.Map<RemoveRoleFromUserCommand>(setRoleToUserDto);
            //command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }
    }
}