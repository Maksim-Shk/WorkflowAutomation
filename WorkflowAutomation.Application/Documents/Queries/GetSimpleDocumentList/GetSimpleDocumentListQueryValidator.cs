using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList;

public class GetSimpleDocumentListQueryValidator : AbstractValidator<GetSimpleDocumentListQuery>
{
    public GetSimpleDocumentListQueryValidator()
    {
        RuleFor(GetSimpleDocumentListQueryValidator => GetSimpleDocumentListQueryValidator.UserId).NotEmpty();
    }
}
