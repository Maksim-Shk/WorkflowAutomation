using FluentValidation;


namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentsStatistics
{
    public class GetDocumentsStatisticsQueryValidator : AbstractValidator<GetDocumentsStatisticsQuery>
    {
        public GetDocumentsStatisticsQueryValidator()
        {
            RuleFor(getDocumentListQuery => getDocumentListQuery.UserId).NotEmpty();
        }
    }
}
