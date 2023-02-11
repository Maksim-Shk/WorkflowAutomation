using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace WorkflowAutomation.Application.Documents.Commands.CreateNewDocument
{
    public class CreateNewSubdivisionCommand : IRequest<int>
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int? SubordinationId { get; set; }
        public List<SubUsers>? SubdivisionUsers { get; set; }

    }
}
