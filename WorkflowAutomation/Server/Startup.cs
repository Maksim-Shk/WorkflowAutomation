using WorkflowAutomation.Application;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Persistence;
using WorkflowAutomation.Server.Middleware;
using WorkflowAutomation.Server.Data;
using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Server.Extensions;

using System.Text;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using AutoMapper;
using MediatR;


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
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>();

            // services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy =>
            //    policy.RequireRole("Админ"));
            //    //policy.RequireClaim("Админ"));
            //    options.AddPolicy("RegisterUserPolicy", policy =>
            //    policy.RequireRole("Зарегистрированный пользователь"));
            //});
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = true,
                          ValidateAudience = true,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,
                          ValidIssuer = Configuration["JwtIssuer"],
                          ValidAudience = Configuration["JwtAudience"],
                          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSecurityKey"]))
                      };
                  });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.ConfigureSwagger();
            services.AddSwaggerGen();

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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1.0/swagger.json", "Main API");
            });

            app.UseCors("AllowAll");
            //app.UseIdentityServer();
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
