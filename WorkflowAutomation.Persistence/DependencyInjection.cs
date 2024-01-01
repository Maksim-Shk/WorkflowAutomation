using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using WorkflowAutomation.Application.Interfaces;

namespace WorkflowAutomation.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection
        services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DbConnection");
        services.AddDbContext<DocumentsDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IDocumentUserDbContext>(provider =>
            provider.GetService<DocumentsDbContext>());
        return services;
    }
}
