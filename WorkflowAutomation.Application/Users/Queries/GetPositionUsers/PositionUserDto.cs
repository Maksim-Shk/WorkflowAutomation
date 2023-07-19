using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkflowAutomation.Application.Common.Mappings;

namespace WorkflowAutomation.Application.Users.Queries.GetPositionUsers
{
    public class PositionUserDto
    {
        public string IdUser { get; set; }
        public string FullName { get; set; }
        public string SubdivisionName { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}
