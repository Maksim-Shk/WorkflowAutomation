using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Documents.Queries.GetSubdivisionList
{
    public class GetSubdivisionListQuery : IRequest<SubdivisionListVm>
    {
        public string UserId { get; set; }
    }
}
