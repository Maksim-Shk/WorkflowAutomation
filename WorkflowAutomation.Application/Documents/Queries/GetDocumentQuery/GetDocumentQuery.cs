using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetOneDocument
{
    public class GetDocumentQuery : IRequest<DocumentDto>
    {
        public int DocumentId { get; set; }
        public string UserId { get; set; }
    }
}
