using MediatR;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionUsersInfo;

public class SubdivisionUsersInfoQuery : IRequest<SubdivisionUsersInfoVm>
{
    public string UserId { get; set; }
}
