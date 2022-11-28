using System;
using FluentValidation;
using WorkflowAutomation.Application.Documents.Commands.UserInfoCommand;

namespace WorkflowAutomation.Application.Roles.Commands.SetRoleToUser
{
    public class SetRoleToUserCommandValidator : AbstractValidator<SetRoleToUserCommand>
    {
        public SetRoleToUserCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.RoleId).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
        }
    }
}