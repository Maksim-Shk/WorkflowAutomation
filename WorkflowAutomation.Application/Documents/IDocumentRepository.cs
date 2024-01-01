using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Documents;

public interface IDocumentRepository
{
    Task<List<AppUser>> GetAllowedUsers(string UserId);
}
