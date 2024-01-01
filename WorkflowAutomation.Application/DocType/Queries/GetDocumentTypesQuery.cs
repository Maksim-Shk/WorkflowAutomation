using MediatR;

namespace WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery;

public class GetDocumentTypesQuery : IRequest<DocumentTypeListVm>
{
    public string UserId { get; set; }
}