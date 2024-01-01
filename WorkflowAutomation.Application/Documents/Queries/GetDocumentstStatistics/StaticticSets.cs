namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentsStatistics;

public class StatisticSets
{
    public string GroupName { get; set; }

    public List<DocumentStatisticsDto> StatisticSet { get; set; }
}
