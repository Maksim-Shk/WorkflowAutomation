namespace WorkflowAutomation.Application.Users.Queries.GetFullUserInfo
{
    public class FullUserInfoDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public int SubdivisionId { get; set; }
        public string SubdivisionName { get; set; }
        public int PositonId { get; set; }
        public string PositonName { get; set; }
        public string RoleName { get; set; }
        public List<UserSubdivisionHistory> UserSubdivisionHistory { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastOnlineDate { get; set; }
    }
}
