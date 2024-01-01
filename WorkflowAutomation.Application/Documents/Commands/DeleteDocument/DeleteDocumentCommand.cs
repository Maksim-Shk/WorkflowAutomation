using MediatR;

namespace WorkflowAutomation.Application.Documents.Commands.DeleteDocument;

public class DeleteDocumentCommand : IRequest
{
    public string UserId { get; set; }
    public int DocumentId { get; set; }
}
