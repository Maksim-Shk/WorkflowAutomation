using FluentValidation;

namespace WorkflowAutomation.Application.Roles.Commands.RemoveRoleFromUser;

public class RemoveRoleFromUserCommandValidator : AbstractValidator<RemoveRoleFromUserCommand>
{
    public RemoveRoleFromUserCommandValidator()
    {
        RuleFor(deleteNoteCommand => deleteNoteCommand.RoleId).NotEmpty();
        RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
    }
}