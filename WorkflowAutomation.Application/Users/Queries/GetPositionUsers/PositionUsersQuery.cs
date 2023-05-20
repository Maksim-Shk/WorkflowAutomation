using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Users.Queries.GetPositionUsers
{
    public class PositionUsersQuery : IRequest<PositionUsersListVm>
    {
        public int PositionId { get; set; }
    }
}
