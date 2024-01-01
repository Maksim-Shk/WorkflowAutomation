using WorkflowAutomation.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WorkflowAutomation.Server.Data;

public class AuthDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public AuthDbContext(
        DbContextOptions<AuthDbContext> options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }
}