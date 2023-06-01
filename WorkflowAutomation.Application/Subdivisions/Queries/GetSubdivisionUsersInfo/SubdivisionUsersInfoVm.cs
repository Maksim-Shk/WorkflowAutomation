namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionUsersInfo
{
    public class SubdivisionUsersInfoVm
    {
        public string SubdivisionName { get; set; }
        public int SubdivisionId { get; set; }
        public IList<SubdivisionUsersInfoDto> Users { get; set; }
    }
}
