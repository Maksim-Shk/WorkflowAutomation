using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Commands.DeleteDocument;

public class DeleteNoteCommandValidator : AbstractValidator<DeleteDocumentCommand>
{
    public DeleteNoteCommandValidator()
    {
        RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
        RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
    }
}
