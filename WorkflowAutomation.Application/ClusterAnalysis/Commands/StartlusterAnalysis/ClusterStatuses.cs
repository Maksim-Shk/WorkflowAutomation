using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis
{
    public class ClusterStatus
    {
        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public double StatusValue { get; set; }
        public double StatusNormaliseValue { get; set; }
    }
}
