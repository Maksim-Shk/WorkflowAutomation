using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocument
{
    public class GetDocumentQuery : IRequest<GetDocumentDto>
    {
        public int DocumentId { get; set; }
        public string UserId { get; set; }
    }
}
