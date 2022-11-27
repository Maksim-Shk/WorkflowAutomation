
//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using Microsoft.AspNetCore.Mvc.ApiExplorer;

//using Swashbuckle.AspNetCore.SwaggerGen;

//using WorkflowAutomation.Server.Services;
//using Serilog.Events;
//using Serilog;
//
using MediatR;

using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

using WorkflowAutomation.Application;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Persistence;
using WorkflowAutomation.Server.Middleware;
using WorkflowAutomation.Server.Data;
using WorkflowAutomation.Server.Models;


namespace WorkflowAutomation.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IDocumentUserDbContext).Assembly));
            });
            services.AddApplication();
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // services.AddMediatR(Assembly.GetExecutingAssembly());

            var connectionString = Configuration["DbConnection"];
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddPersistence(Configuration);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            //Это ОК
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();
            // services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                policy.RequireRole("Админ"));
                //policy.RequireClaim("Админ"));
                options.AddPolicy("RegisterUserPolicy", policy =>
                policy.RequireRole("Зарегистрированный пользователь"));
            });
            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, AuthDbContext>(options =>
                {
                    options.IdentityResources["openid"].UserClaims.Add("role");
                    options.ApiResources.Single().UserClaims.Add("role");
                }
                );

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

            // services.AddSingleton<ICurrentUserService, CurrentUserService>();

            //services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
                app.UseDeveloperExceptionPage();
            }
            else { app.UseHsts(); }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
