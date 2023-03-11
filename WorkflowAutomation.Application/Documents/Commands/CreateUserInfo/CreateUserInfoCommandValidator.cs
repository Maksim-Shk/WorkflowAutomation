using System;
using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Commands.UserInfoCommand
{
    public class CreateUserInfoCommandValidator : AbstractValidator<CreateUserInfoCommand>
    {
        public CreateUserInfoCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Name).Length(1, 256);
            RuleFor(deleteNoteCommand => deleteNoteCommand.Surname).Length(1, 256);
            RuleFor(deleteNoteCommand => deleteNoteCommand.Patronymic).Length(1, 256);
            RuleFor(deleteNoteCommand => deleteNoteCommand.IdSubdivision).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(deleteNoteCommand => deleteNoteCommand.IdPositon).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
        }
    }
}
