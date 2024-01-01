using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetAllowedDocumentList;

public class GetAllowedDocumentListQuery : IRequest<AllowedDocumentListVm>
{
    public string UserId { get; set; }
}
