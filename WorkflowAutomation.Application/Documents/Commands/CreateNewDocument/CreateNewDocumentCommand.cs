using System;
using MediatR;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewDocumentCommand : IRequest<int>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        //public int RouteId { get; set; }
        //public int StatusId { get; set; }
        public int DocumentTypeId { get; set; }
       // public int PreviousDocumentUser { get; set; }
        public Guid ReceiverUserId { get; set; } //Возможно сделать множественную рассылку

    }
}
