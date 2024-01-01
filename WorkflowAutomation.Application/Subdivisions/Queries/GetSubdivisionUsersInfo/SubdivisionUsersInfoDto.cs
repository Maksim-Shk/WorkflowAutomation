namespace WorkflowAutomation.Application.Subdivisions.Queries.GetSubdivisionUsersInfo;

public class SubdivisionUsersInfoDto
{
    public string FullName { get; set; }
    public string UserId { get; set; }
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public string SubdivisionName { get; set; }
    public string PositionName { get; set; }
    public DateTime DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public UserStatus UserStatus { get; set; }
}
