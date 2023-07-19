using MediatR;

namespace WorkflowAutomation.Application.Positions.Queries.GetPositionList
{
    public class GetPositionListQuery : IRequest<PositionListVm>
    {
        public string UserId { get; set; }
    }
}
