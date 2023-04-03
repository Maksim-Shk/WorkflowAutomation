using MediatR;
namespace WorkflowAutomation.Application.Roles.Queries.GetUserRolesList
{
    public class GetUserRolesListQuery : IRequest<UserRolesListVm>
    {
        public string UserId { get; set; }
    }
}
