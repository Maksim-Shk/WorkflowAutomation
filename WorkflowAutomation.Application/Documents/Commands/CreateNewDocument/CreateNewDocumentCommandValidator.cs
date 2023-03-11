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
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.DocumentTypeId).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.UserId).NotEmpty();
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.Uri).NotEmpty();
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.ContentRootPath).NotEmpty();
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.EnvironmentName).NotEmpty();
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.jwtToken).NotEmpty();
            //TODO: вынести лимит и размер файлов в настройки
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.MaxAllowedFiles).GreaterThanOrEqualTo(10);
            RuleFor(createNewDocumentCommand => createNewDocumentCommand.MaxFileSize).GreaterThanOrEqualTo(15 * 1024 * 1024);
        }
    }
}
