using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionUsersInfo
{
    public class SubdivisionUsersInfoQuery : IRequest<SubdivisionUsersInfoVm>
    {
        public string UserId { get; set; }
    }
}
