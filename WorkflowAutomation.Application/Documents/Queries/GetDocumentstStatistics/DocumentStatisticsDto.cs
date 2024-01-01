namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentsStatistics;

public class DocumentStatisticsDto
{
    public string Title { get; set; }
    public DateTime AppointmentDate { get; set; }
    public TimeSpan StatusTime { get; set; }
    public int SubdivisionId { get; set; }
    public string SubdivisionName { get; set; }
    public int PositionId { get; set; }
    public string PositionName { get; set; }
}
