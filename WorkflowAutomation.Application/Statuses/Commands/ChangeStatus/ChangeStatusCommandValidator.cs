using FluentValidation;

namespace WorkflowAutomation.Application.Statuses.Commands.ChangeStatus;

public class ChangeStatusCommandValidator : AbstractValidator<ChangeStatusCommand>
{
    public ChangeStatusCommandValidator()
    {
        RuleFor(deleteNoteCommand => deleteNoteCommand.DocumentId).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
        RuleFor(deleteNoteCommand => deleteNoteCommand.StatusId).NotNull().GreaterThanOrEqualTo(0);
        RuleFor(deleteNoteCommand => deleteNoteCommand.JwtToken).NotEmpty();
        RuleFor(deleteNoteCommand => deleteNoteCommand.Uri).NotEmpty();
    }
}