using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetPositionList
{
    public class GetPositionListQuery : IRequest<PositionListVm>
    {
        public string UserId { get; set; }
    }
}
