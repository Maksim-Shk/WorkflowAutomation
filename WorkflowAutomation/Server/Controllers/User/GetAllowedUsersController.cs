using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WorkflowAutomation.Application.Users.Queries.GetAllowedUsers;

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GetAllowedUsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllowedUsersController> _logger;

        public GetAllowedUsersController(
           IMapper mapper,
            ILogger<GetAllowedUsersController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<AllowedUserListVm>> GetAllowedUsers()
        {
            var query = new GetAllowedUserListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
