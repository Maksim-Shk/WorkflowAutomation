using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewDocumentCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        //public int RouteId { get; set; }
        //public int StatusId { get; set; }
        public int DocumentTypeId { get; set; }
        // public int PreviousDocumentUser { get; set; }
        public string ReceiverUserId { get; set; } //Возможно сделать множественную рассылку
        public Uri? resourcePath { get; set; }
        public IEnumerable<IFormFile> Files { get; set; }
        public string ContentRootPath { get; set; }
        public string EnvironmentName { get; set; }
        public int MaxAllowedFiles { get; set; }
        public long MaxFileSize { get; set; }

    }
}
