using FluentValidation;

namespace WorkflowAutomation.Application.Subdivisions.Commands.CreateNewSubdivision;

public class ChangeStatusCommandValidator : AbstractValidator<CreateNewSubdivisionCommand>
{
    public ChangeStatusCommandValidator()
    {
        RuleFor(deleteNoteCommand => deleteNoteCommand.Name).NotEmpty();
        RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEmpty();
        RuleFor(deleteNoteCommand => deleteNoteCommand.CreateDate)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage(deleteNoteCommand => $"��������� ���� �������� {deleteNoteCommand.CreateDate} ������ �������: {DateTime.Now}. ��� �����������");
    }
}
