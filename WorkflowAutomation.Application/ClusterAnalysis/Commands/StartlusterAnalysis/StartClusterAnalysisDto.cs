using AutoMapper;
using System;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis;

namespace WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis
{
    public class StartClusterAnalysisDto : IMapWith<ClusterAnalysisCommand>
    {
        /// <summary>
        /// Количество кластеров
        /// </summary>
        public int ClusterCount { get; set; }
        public List<int> StatusesIds { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StartClusterAnalysisDto, ClusterAnalysisCommand>()
                     .ForMember(task => task.ClusterCount,
                   opt => opt.MapFrom(task => task.ClusterCount))
                     .ForMember(task => task.StatusesIds,
                   opt => opt.MapFrom(task => task.StatusesIds));
        }
    }
}
