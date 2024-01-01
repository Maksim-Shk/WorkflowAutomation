using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetRecentActivityDocuments;

public class GetRecentActivityDocumentsQuery : IRequest<RecentActivityDocumentListVm>
{
    /// <summary>
    /// ID запрашивающего пользователя
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// Количество выдаваемых документов для пагинации
    /// </summary>
    public int NumberOfEntity { get; set; }
}
