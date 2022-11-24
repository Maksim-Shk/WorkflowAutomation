using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Commands.DeleteDocument
{
    public class DeleteNoteCommandValidator : AbstractValidator<DeleteDocumentCommand>
    {
        public DeleteNoteCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.DocumentId).NotEmpty().GreaterThan(0);
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
        }
    }
}
