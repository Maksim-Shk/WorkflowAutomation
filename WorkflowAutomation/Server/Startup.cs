using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Reflection;
//using Swashbuckle.AspNetCore.SwaggerGen;
using WorkflowAutomation.Application;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Persistence;
using WorkflowAutomation.Server.Middleware;
using WorkflowAutomation.Server.Services;
using Serilog.Events;
using Serilog;
using Microsoft.EntityFrameworkCore;
using WorkflowAutomation.Server.Data;
using Microsoft.AspNetCore.Authentication;
using WorkflowAutomation.Server.Models;

namespace WorkflowAutomation.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IDocumentUserDbContext).Assembly));
            });

            services.AddApplication();
            services.AddPersistence(Configuration);
            var connectionString = Configuration["DbConnection"];
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AuthDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, AuthDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

           // services.AddAuthentication(config =>
           // {
           //     config.DefaultAuthenticateScheme =
           //         JwtBearerDefaults.AuthenticationScheme;
           //     config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           // })
           //     .AddJwtBearer("Bearer", options =>
           //     {
           //         options.Authority = "https://localhost:7153/";
           //         options.Audience = "WorkflowAutomationWebAPI";
           //         options.RequireHttpsMetadata = false;
           //     });

            //services.AddVersionedApiExplorer(options =>
            //    options.GroupNameFormat = "'v'VVV");
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
            //        ConfigureSwaggerOptions>();
            //  services.AddSwaggerGen();
            //  services.AddApiVersioning();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env
            //,IApiVersionDescriptionProvider provider
            )
        { 
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
                // app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // app.UseSwagger();
            // app.UseSwaggerUI(config =>
            // {
            //     foreach (var description in provider.ApiVersionDescriptions)
            //     {
            //         config.SwaggerEndpoint(
            //             $"/swagger/{description.GroupName}/swagger.json",
            //             description.GroupName.ToUpperInvariant());
            //         config.RoutePrefix = string.Empty;
            //     }
            // });
            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();
            // app.UseApiVersioning();
         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
