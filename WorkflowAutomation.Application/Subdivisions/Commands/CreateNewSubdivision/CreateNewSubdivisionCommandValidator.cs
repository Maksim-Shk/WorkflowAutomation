using FluentValidation;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;

namespace WorkflowAutomation.Application.Subdivisions.Commands.CreateNewSubdivision
{
    public class ChangeStatusCommandValidator : AbstractValidator<CreateNewSubdivisionCommand>
    {
        public ChangeStatusCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Name).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.CreateDate).LessThanOrEqualTo(DateTime.Now)
               .WithMessage(deleteNoteCommand => $"Выбранная дата создания {deleteNoteCommand.CreateDate} больше текущей: {DateTime.Now}. Это недопустимо");
            //RuleFor(deleteNoteCommand => deleteNoteCommand.SubordinationId).NotNull();
            //RuleFor(deleteNoteCommand => deleteNoteCommand.SubdivisionUsers).NotEmpty();
        }
    }
}
