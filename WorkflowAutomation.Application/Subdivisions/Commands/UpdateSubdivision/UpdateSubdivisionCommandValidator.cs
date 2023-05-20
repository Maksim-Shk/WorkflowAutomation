using FluentValidation;

namespace WorkflowAutomation.Application.Subdivisions.Commands.UpdateSubdivision
{
    public class UpdateSubdivisionCommandValidator : AbstractValidator<UpdateSubdivisionCommand>
    {
        public UpdateSubdivisionCommandValidator()
        {
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
            RuleFor(deleteNoteCommand => deleteNoteCommand.SubdivisionId).NotNull().GreaterThan(0);
            RuleFor(deleteNoteCommand => deleteNoteCommand.CreateDate).LessThanOrEqualTo(DateTime.Now)
               .WithMessage(deleteNoteCommand => $"��������� ���� �������� {deleteNoteCommand.CreateDate} ������ �������: {DateTime.Now}. ��� �����������");
        }
    }
}
