using System;
using MediatR;

namespace WorkflowAutomation.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersListQuery : IRequest<AllUsersListVm>
    {
        public string UserId { get; set; }
    }
}
