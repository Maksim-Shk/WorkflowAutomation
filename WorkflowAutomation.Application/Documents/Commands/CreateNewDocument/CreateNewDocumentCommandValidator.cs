using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewDocumentCommandValidator : AbstractValidator<CreateNewDocumentCommand>
    {
        public CreateNewDocumentCommandValidator()
        {
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.Title).Length(1, 256);
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.ReceiverUserId).NotEmpty();
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.DocumentTypeId).GreaterThanOrEqualTo(0);
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.UserId).NotEmpty();
        }
    }
}
