using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Interfaces
{
    public interface IDocumentsDbContext
    {
        DbSet<Document> Documents { get; set; }
        Task<int> Save(CancellationToken cancellationToken);
    }
}
