using System;
using MediatR;

namespace WorkflowAutomation.Application.Users.Queries.GetAllowedUsers
{
    public class GetAllowedUserListQuery : IRequest<AllowedUserListVm>
    {
        public string UserId { get; set; }
    }
}
