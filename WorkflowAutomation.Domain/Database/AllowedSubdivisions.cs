using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Domain.Database
{
    public class AllowedSubdivisions
    {
        public int IdSubdivision { get; set; }
        public string Name { get; set; } 
        public int? IdSubordination { get; set; }
    }
}
