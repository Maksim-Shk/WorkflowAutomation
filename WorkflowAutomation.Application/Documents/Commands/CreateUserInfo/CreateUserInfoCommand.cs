using System;
using MediatR;

namespace WorkflowAutomation.Application.Documents.Commands.UserInfoCommand
{
    public class CreateUserInfoCommand : IRequest<string>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int IdSubdivision { get; set; }
        public int IdPositon { get; set; } 

    }
}
