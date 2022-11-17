using AutoMapper;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Documents.Commands.CreateNewDocument;
using System.ComponentModel.DataAnnotations;

namespace WorkflowAutomation.Shared
{
    public class LoadAllDocuments 
    {
        public string Title { get; set; }
        public string DocumentType { get; set; }
        public string ReceiverUser { get; set; }

    } 
}
