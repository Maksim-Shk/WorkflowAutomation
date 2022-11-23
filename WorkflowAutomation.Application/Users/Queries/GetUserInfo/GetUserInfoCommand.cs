using System;
using MediatR;

namespace WorkflowAutomation.Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoCommand : IRequest<GetUserInfoDto>
    {
        public string UserId { get; set; }
    }
}
