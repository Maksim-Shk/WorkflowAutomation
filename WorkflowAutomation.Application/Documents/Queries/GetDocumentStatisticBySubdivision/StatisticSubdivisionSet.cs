namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentStatisticBySubdivision;

public class StatisticSubdivisionSet
{
    public string GroupName { get; set; }

    public List<DocumentBySubdivisionDto> StatisticSet { get; set; }
}
