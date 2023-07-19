using MediatR;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentStatisticBySubdivision
{
    public class GetDocumentStatisticBySubdivisionQuery : IRequest<DocumentStatisticBySubdivisionListVm>
    {
        public string UserId { get; set; }
    }
}
