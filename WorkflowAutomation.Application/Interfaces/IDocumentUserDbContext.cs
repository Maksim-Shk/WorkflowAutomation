using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Domain;

namespace WorkflowAutomation.Application.Interfaces
{
    /// <summary>
    /// Используется везде
    /// </summary>
    public interface IDocumentUserDbContext
    {
        DbSet<Document> Documents { get; set; }
        DbSet<DocumentContent> DocumentContents { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<DocumentStatus> DocumentStatuses { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Status> Statuses { get; set; }
        DbSet<Subdivision> Subdivisions { get; set; }
        DbSet<AppUser> Users { get; set; }
        DbSet<UserPosition> UserPositions { get; set; }
        DbSet<UserSubdivision> UserSubdivisions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
