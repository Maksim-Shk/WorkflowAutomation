using MediatR;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo
{
    public class GetSubdivisionInfoQuery : IRequest<SubdivisionInfoDto>
    {
        public string UserId { get; set; }
        public int SubdivisionId { get; set; }
    }
}
