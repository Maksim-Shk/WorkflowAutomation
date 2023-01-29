using FluentValidation;

namespace WorkflowAutomation.Application.Documents.Queries.GetAllowedDocumentList
{
    public class GetAllowedDocumentListQueryValidator : AbstractValidator<GetAllowedDocumentListQuery>
    {
        public GetAllowedDocumentListQueryValidator()
        {
            RuleFor(getAllowedDocumentListQuery => getAllowedDocumentListQuery.UserId).NotEmpty();
        }
    }
}
