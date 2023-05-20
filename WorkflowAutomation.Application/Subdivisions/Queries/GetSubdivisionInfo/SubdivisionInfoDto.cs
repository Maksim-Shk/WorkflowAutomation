
namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionInfo
{
    public class SubdivisionInfoDto
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int? SubordinationId { get; set; }
        public string? SubordinationName { get; set; }
        public List<SubdivisionUser> Users { get; set; }
    }
}