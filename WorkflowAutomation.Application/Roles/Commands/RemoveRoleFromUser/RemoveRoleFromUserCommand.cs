using System;
using MediatR;

namespace WorkflowAutomation.Application.Roles.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserCommand : IRequest
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        //TODO: public string WhoGaveRoleId { get; set; }
    }
}
