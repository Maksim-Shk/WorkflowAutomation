namespace WorkflowAutomation.Client.Models
{
    public class UserInfo
    {
        public string? Name { get; set; }

        public string? Patronomic { get; set; }

        public bool IsInfoFull { get; set; } = false;

        public void Clear()
        {
            Name = null; 
            Patronomic = null; 
            IsInfoFull = false;
        }
    }
}
