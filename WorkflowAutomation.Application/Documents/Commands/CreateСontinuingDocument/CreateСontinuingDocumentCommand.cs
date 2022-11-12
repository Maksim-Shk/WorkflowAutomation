using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Commands.CreateСontinuingDocument
{
    public class CreateСontinuingDocumentCommand : IRequest<int>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        //public int RouteId { get; set; }
        //public int StatusId { get; set; }
        public int DocumentTypeId { get; set; }
        public int PreviousDocumentUserId { get; set; }
        public Guid ReceiverUser { get; set; } //Возможно сделать множественную рассылку
    }
}
