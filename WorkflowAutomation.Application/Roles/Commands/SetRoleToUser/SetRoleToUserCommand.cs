using System;
using MediatR;

namespace WorkflowAutomation.Application.Roles.Commands.SetRoleToUser
{
    public class SetRoleToUserCommand : IRequest
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        //TODO: public string WhoGaveRoleId { get; set; }
    }
}
