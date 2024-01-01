using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments;

public class GetRecentActivityDocumentsQueryValidator : AbstractValidator<GetRecentActivityDocumentsQuery>
{
    public GetRecentActivityDocumentsQueryValidator()
    {
        RuleFor(getDocumentListQuery => getDocumentListQuery.UserId).NotEmpty();
        RuleFor(getDocumentListQuery => getDocumentListQuery.NumberOfEntity).NotEmpty().GreaterThanOrEqualTo(1);
    }
}
