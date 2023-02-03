namespace WorkflowAutomation.Client.Models
{
    public class UserInfo
    {
        public string? Name { get; set; }

        public string? Patronomic { get; set; }

        public bool IsInfoFull { get; set; } = false;

        public string? UserId { get; set; }  

        public void Clear()
        {
            Name = null; 
            Patronomic = null; 
            IsInfoFull = false;
            UserId = null;
            
        }
    }
}
