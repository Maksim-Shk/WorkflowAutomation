using MediatR;

namespace WorkflowAutomation.Application.Subdivisions.Commands.CreateNewSubdivision;

public class CreateNewSubdivisionCommand : IRequest<int>
{
    public string UserId { get; set; }
    public string Name { get; set; }
    public DateTime CreateDate { get; set; }
    public int? SubordinationId { get; set; }
    public List<SubUsers>? SubdivisionUsers { get; set; }

}
