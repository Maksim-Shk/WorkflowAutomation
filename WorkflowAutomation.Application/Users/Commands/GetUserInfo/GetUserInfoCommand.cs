using System;
using MediatR;

namespace WorkflowAutomation.Application.Users.Commands.GetUserInfo
{
    public class GetUserInfoCommand : IRequest<GetUserInfoDto>
    {
        public string UserId { get; set; }
    }
}
