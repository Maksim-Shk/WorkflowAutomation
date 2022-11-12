using System;
using MediatR;

namespace WorkflowAutomation.Application.Documents.Commands.CompleteDocument
{
    public class CompleteDocumentCommand : IRequest
    {
        public Guid UserId { get; set; }
        public int DocumentId { get; set; }
    }
}
