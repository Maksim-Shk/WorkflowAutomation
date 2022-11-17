using Microsoft.EntityFrameworkCore;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Domain;
using WorkflowAutomation.Persistence.EntityTypeConfigurations;

namespace WorkflowAutomation.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}