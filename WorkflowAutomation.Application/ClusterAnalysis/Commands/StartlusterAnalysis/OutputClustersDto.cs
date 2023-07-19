using AutoMapper;

namespace WorkflowAutomation.Application.ClusterAnalysis.Commands.StartlusterAnalysis
{
    public class OutputClustersDto
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public int ClusterId { get; set; }
        public string ClusterName { get; set; }
        public List<ClusterStatus> Statuses { get; set; }
        public string DocumentType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}