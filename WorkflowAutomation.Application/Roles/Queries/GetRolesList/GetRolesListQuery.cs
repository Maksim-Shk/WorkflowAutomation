using MediatR;

namespace WorkflowAutomation.Application.Roles.Queries.GetRolesList;

public class GetRolesListQuery : IRequest<RolesListVm>
{
    public string UserId { get; set; }
}
