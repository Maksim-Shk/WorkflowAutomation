using WorkflowAutomation.Application;
using WorkflowAutomation.Persistence;
using WorkflowAutomation.Server.Data;
using WorkflowAutomation.Server.Models;
using WorkflowAutomation.Server.Middleware;
using WorkflowAutomation.Server.Extensions;
using WorkflowAutomation.Application.Interfaces;
using WorkflowAutomation.Application.Common.Mappings;
using WorkflowAutomation.Server.Options;
using WorkflowAutomation.Application.Documents;
using WorkflowAutomation.Server.Hubs;

using System.Text;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using IdentityModel;

namespace WorkflowAutomation.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazorContextMenu();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IDocumentUserDbContext).Assembly));
            });
            services.AddApplication();
            //Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            // services.AddMediatR(Assembly.GetExecutingAssembly());

           var connectionString = Configuration.GetConnectionString("DbConnection");
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

            services
                .AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<AuthDbContext>();

            //services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>>();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdminPolicy", policy =>
            //    policy.RequireRole("Админ"));
            //    //policy.RequireClaim("Админ"));
            //    options.AddPolicy("RegisterUserPolicy", policy =>
            //    policy.RequireRole("Зарегистрированный пользователь"));
            //});

            services.AddScoped<IDocumentRepository, DocumentRepository>();

            services.AddOptions();
            services.Configure<AuthJwtOptions>(Configuration.GetSection("JwtSettings"));

            var authSettings = Configuration.GetSection("JwtSettings").Get<AuthJwtOptions>();

            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authSettings.JwtIssuer,
                        ValidAudience = authSettings.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.JwtSecurityKey))
                    };
                });
            //SignalR
            //services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.ConfigureSwagger();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //SignalR
            app.UseResponseCompression();

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
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                //endpoints.MapDefaultControllerRoute();
              
                endpoints.MapHub<NotificationHub>("/notificationHub"); //SignalR
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
