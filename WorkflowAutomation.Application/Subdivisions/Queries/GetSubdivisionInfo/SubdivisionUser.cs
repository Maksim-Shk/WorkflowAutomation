
namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo
{
    public class SubdivisionUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int PositionId { get; set; }
        public bool IsRender { get; set; } = false;

        public DateTime AppointmentDate {get;set;}
    }
}