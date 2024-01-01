using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkflowAutomation.Application.Documents.Commands.UserInfoCommand;
using WorkflowAutomation.Application.Users.Queries.GetUserInfo;
using WorkflowAutomation.Application.Users.Queries.GetPositionUsers;

namespace WorkflowAutomation.Server.Controllers;

[Authorize]
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
    public async Task<ActionResult<string>> CreateUserInfo([FromBody] CreateUserInfoDto createUsertDto)
    {
        //Task<ActionResult<Guid> 
        var command = _mapper.Map<CreateUserInfoCommand>(createUsertDto);
        command.UserId = UserId.ToString();

        var userId = await Mediator.Send(command);

        return Ok(userId);
    }

    [HttpGet]
    public async Task<ActionResult<GetUserInfoDto>> GetUserInfo()
    {
        var command = new GetUserInfoQuery
        {
            UserId = UserId
        };
        var dto = await Mediator.Send(command);
        return Ok(dto);
    }

    [HttpGet("PositionUsers/{posId}")]
    public async Task<ActionResult<PositionUsersListVm>> GetPositionUsers(int posId)
    {
        var command = new PositionUsersQuery
        {
            PositionId = posId
        };
        var dto = await Mediator.Send(command);
        return Ok(dto);
    }
}
