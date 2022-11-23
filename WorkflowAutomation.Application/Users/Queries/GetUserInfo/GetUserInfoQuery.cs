using System;
using MediatR;

namespace WorkflowAutomation.Application.Users.Queries.GetUserInfo
{
    public class GetUserInfoQuery : IRequest<GetUserInfoDto>
    {
        public string UserId { get; set; }
    }
}
