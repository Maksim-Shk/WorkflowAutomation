using FluentValidation;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class ChangeStatusCommandValidator : AbstractValidator<CreateNewSubdivisionCommand>
    {
        public ChangeStatusCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Name).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.CreateDate).NotNull();
            RuleFor(deleteNoteCommand => deleteNoteCommand.SubordinationId).NotNull();
            RuleFor(deleteNoteCommand => deleteNoteCommand.SubdivisionUsers).NotEmpty();
        }
    }
}
