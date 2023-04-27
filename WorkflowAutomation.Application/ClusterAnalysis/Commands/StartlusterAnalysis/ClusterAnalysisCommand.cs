using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis
{
    public class ClusterAnalysisCommand : IRequest
    {
        /// <summary>
        /// Количество кластеров
        /// </summary>
        public int ClusterCount { get; set; }
        public List<int> StatusesIds { get; set; }

    }
}
