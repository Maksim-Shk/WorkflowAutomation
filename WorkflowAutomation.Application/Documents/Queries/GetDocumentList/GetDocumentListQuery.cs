using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList
{
    public class GetDocumentListQuery : IRequest<DocumentListVm>
    {
        public string UserId { get; set; }
    }
}
