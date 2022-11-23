using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.DocType.Queries.GetDocumentTypeListQuery
{
    public class GetDocumentTypesQuery : IRequest<DocumentTypeListVm>
    {
        public string UserId { get; set; }
    }
}
