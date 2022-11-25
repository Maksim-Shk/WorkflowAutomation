using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments
{
    public class GetRecentActivityDocumentsQuery : IRequest<RecentActivityDocumentListVm>
    {
        public string UserId { get; set; }
    }
}
