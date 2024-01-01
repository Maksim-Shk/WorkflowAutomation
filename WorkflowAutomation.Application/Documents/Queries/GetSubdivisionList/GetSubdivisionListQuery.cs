using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetSubdivisionList;

public class GetSubdivisionListQuery : IRequest<SubdivisionListVm>
{
    public string UserId { get; set; }
}
