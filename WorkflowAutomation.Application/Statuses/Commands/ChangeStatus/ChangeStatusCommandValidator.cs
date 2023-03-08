using System;
using FluentValidation;
using WorkflowAutomation.Application.Documents.Commands.UserInfoCommand;

namespace WorkflowAutomation.Application.Statuses.Commands.ChangeStatus
{
    public class ChangeStatusCommandValidator : AbstractValidator<ChangeStatusCommand>
    {
        public ChangeStatusCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.DocumentId).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.StatusId).NotEmpty();
        }
    }
}