using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList;

public class GetSimpleDocumentListQuery  : IRequest<SimpleDocumentListVm>
{
    public string UserId { get; set; }
}
