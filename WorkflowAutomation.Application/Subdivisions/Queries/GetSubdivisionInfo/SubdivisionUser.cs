
namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo
{
    public class SubdivisionUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int PositionId { get; set; }
        public RenderOption Render { get; set; } = RenderOption.NotRender;
        public DateTime? RemovalDate { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}