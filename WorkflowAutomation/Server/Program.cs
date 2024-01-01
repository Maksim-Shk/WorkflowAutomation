using Serilog;
using WorkflowAutomation.Persistence;

namespace WorkflowAutomation.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
           // Log.Logger = new LoggerConfiguration()
           //     .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
           //     .WriteTo.File("WorkflowAutomationWebAppLog-.txt", rollingInterval:
           //         RollingInterval.Day)
           //     .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<DocumentsDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception, "An error occurred while app initialization");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseWebRoot("wwwroot");
                    //webBuilder.UseStaticWebAssets();
                    webBuilder.UseStartup<Startup>();
                });
    }
}


























//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.ResponseCompression;
//using Microsoft.EntityFrameworkCore;
//using WorkflowAutomation.Server.Data;
//using WorkflowAutomation.Server.Models;
//
//var builder = WebApplication.CreateBuilder(args);
//
//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//
//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//
//builder.Services.AddIdentityServer()
//    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
//
//builder.Services.AddAuthentication()
//    .AddIdentityServerJwt();
//
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();
//
//var app = builder.Build();
//
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//    app.UseWebAssemblyDebugging();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
//
//app.UseHttpsRedirection();
//
//app.UseBlazorFrameworkFiles();
//app.UseStaticFiles();
//
//app.UseRouting();
//
//app.UseIdentityServer();
//app.UseAuthentication();
//app.UseAuthorization();
//
//
//app.MapRazorPages();
//app.MapControllers();
//app.MapFallbackToFile("index.html");
//
//app.Run();
