using System.Collections.Generic;

namespace WorkflowAutomation.Shared.Identity
{
    public class RegisterResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
