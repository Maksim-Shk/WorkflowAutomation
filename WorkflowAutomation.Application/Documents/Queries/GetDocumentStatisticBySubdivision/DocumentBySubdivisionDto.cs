namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentStatisticBySubdivision;

public class DocumentBySubdivisionDto
{
    public string Title { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan AllStatusTime { get; set; }
    public string CurrentStatusName { get; set; }
    public int CurrentStatusId { get; set; }
    public int SubdivisionId { get; set; }
    public string SubdivisionName { get; set; }
    public int PositionId { get; set; }
    public string PositionName { get; set; }
}
