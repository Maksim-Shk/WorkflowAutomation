using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentsStatistics;

public class GetDocumentsStatisticsQuery : IRequest<DocumentStatisticsListVm>
{
    public string UserId { get; set; }

    public List<int> StatusIds { get; set; }
}
