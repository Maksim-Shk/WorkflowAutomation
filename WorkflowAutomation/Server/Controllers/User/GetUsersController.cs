using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WorkflowAutomation.Application.Users.Queries.GetAllowedUsers;
using WorkflowAutomation.Application.Users.Queries.GetAllUsers; 

namespace WorkflowAutomation.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class GetUsersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetUsersController> _logger;

        public GetUsersController(
           IMapper mapper,
            ILogger<GetUsersController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }
        /// <summary>
        /// Возвращает всех пользователей, кроме запрашивающего
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllUsers")]
        [Authorize]
        public async Task<ActionResult<AllUsersListVm>> GetAllUsersExceptRequest()
        {
            var query = new GetAllUsersListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}
