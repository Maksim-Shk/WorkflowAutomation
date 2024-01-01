using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Domain.Database;

namespace WorkflowAutomation.Application.Interfaces;

/// <summary>
/// Используется везде
/// </summary>
public interface IDocumentUserDbContext
{
    DbSet<AppUser> Users { get; set; } 
    DbSet<Document> Documents { get; set; } 
    DbSet<DocumentContent> DocumentContents { get; set; } 
    DbSet<DocumentStatus> DocumentStatuses { get; set; } 
    DbSet<DocumentType> DocumentTypes { get; set; } 
    DbSet<Position> Positions { get; set; } 
    DbSet<Status> Statuses { get; set; } 
    DbSet<Subdivision> Subdivisions { get; set; } 
    DbSet<UserPosition> UserPositions { get; set; }
    DbSet<UserSubdivision> UserSubdivisions { get; set; }
    DbSet<AspNetRole> AspNetRoles { get; set; } 
    DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } 
    DbSet<AspNetUser> AspNetUsers { get; set; }
    DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } 
    DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } 
    DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
    List<AllowedSubdivision> GetAllowedSubdivisions(int a);

    Task<int> Save(CancellationToken cancellationToken);
    DatabaseFacade Database { get { return this.Database; } }
}
