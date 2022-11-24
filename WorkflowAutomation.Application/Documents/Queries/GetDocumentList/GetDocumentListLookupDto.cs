using AutoMapper;
using System;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList
{
    public class GetDocumentListLookupDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? RemoveDate { get; set; }
        public string DocumentType { get; set; }
        public string SenderInfo { get; set; }
        public string RecieverInfo { get; set; }
        //TODO ещё чего

    }
}
