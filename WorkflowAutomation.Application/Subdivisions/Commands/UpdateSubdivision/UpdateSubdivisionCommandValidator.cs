using FluentValidation;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision
{
    public class UpdateSubdivisionCommandValidator : AbstractValidator<UpdateSubdivisionCommand>
    {
        public UpdateSubdivisionCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.Name).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.CreateDate).NotNull();
            RuleFor(deleteNoteCommand => deleteNoteCommand.SubordinationId).NotNull();
            RuleFor(deleteNoteCommand => deleteNoteCommand.SubdivisionUsers).NotEmpty();
        }
    }
}
