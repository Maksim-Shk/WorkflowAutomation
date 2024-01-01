using MediatR;

namespace WorkflowAutomation.Application.Users.Queries.GetPositionUsers;

public class PositionUsersQuery : IRequest<PositionUsersListVm>
{
    public int PositionId { get; set; }
}
