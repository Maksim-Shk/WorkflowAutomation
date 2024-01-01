using MediatR;

namespace WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis;

public class ClusterAnalysisCommand : IRequest<OutputClustersVm>
{
    /// <summary>
    /// ���������� ���������
    /// </summary>
    public int ClusterCount { get; set; }
    public List<int> StatusesIds { get; set; }

}
