﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.Positions.Queries.GetPositionList
{
    public class PositionListVm
    {
        public IList<PositionListLookupDto> Positions { get; set; }
    }
}
