using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
using WorkflowAutomation.Application.Documents.Queries.GetPositionList;
using WorkflowAutomation.Application.Users.Queries.GetUserInfo;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserInfoController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserInfoController> _logger;

        public UserInfoController(
           IMapper mapper,
            ILogger<UserInfoController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> CreateUserInfo([FromBody] CreateUserInfoDto createUsertDto)
        {
            //Task<ActionResult<Guid> 
            var command = _mapper.Map<CreateUserInfoCommand>(createUsertDto);
            command.UserId = UserId.ToString();

            var userId = await Mediator.Send(command);

            return Ok(userId);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GetUserInfoDto>> GetUserInfo()
        {
            var command = new GetUserInfoQuery
            {
                UserId = UserId
            };
            var dto = await Mediator.Send(command);
            return Ok(dto);
        }
    }
}
