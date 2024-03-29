﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkflowAutomation.Application.DataGeneration.Commands
{
    public class GenerateDataCommand : IRequest
    {
        public int UserCount { get; set; }
        public List<string> UsersNames { get; set; }
        public int DocumentPerUserCount { get; set; }
        public int AdminCount { get; set; }
        public List<string> AdminsNames { get; set; }
    //    public DateTime StartStatusDate { get; set; }
        public DateTime EndStatusDate { get; set;}

    }
}
