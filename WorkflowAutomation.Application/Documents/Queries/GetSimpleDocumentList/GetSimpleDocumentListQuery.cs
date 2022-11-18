using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Documents.Queries.GetDocumentList
{
    public class GetSimpleDocumentListQuery  : IRequest<SimpleDocumentListVm>
    {
        public string UserId { get; set; }
    }
}
