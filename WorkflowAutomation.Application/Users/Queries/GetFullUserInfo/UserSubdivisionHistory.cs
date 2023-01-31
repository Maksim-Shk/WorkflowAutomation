namespace WorkflowAutomation.Application.Users.Queries.GetFullUserInfo
{
    public class UserSubdivisionHistory
    {
        public string SubdivisionName { get; set; }
        public string PositionName { get; set; }
        public int PositonId { get; set; }
        public int SubdivisionId { get; set; }
        public DateTime? DismissalDate { get; set; }
        public DateTime EmploymentDate { get; set; }
        public string WorkingTime { get; set; }
    }
}
