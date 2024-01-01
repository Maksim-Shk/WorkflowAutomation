using FluentValidation;

namespace WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery;

public class GetDocumentTypesQueryValidator : AbstractValidator<GetDocumentTypesQuery>
{
    public GetDocumentTypesQueryValidator()
    {
        RuleFor(getDocumentTypesQuery => getDocumentTypesQuery.UserId).NotEmpty();
    }
}