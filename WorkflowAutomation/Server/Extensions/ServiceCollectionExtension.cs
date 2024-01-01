using Microsoft.OpenApi.Models;

namespace WorkflowAutomation.Server.Extensions;

public static class ServiceCollectionExtension
{
  // public static void ConfigureIdentity(this IServiceCollection services)
  // {
  //     var builder = services
  //         .AddIdentity<AuthDbContext, IdentityRole>(io =>
  //         {
  //             io.Password.RequireDigit = false;
  //             io.Password.RequireLowercase = false;
  //             io.Password.RequireUppercase = false;
  //             io.Password.RequireNonAlphanumeric = false;
  //             io.User.RequireUniqueEmail = true;
  //         })
  //         .AddEntityFrameworkStores<AuthDbContext>()
  //         .AddDefaultTokenProviders();
  // }

  //  public static void ConfigureJWT(this IServiceCollection services, JWTSettings settings)
  //  {
  //      services
  //          .AddAuthentication(options =>
  //          {
  //              options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  //              options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  //          })
  //          .AddJwtBearer(options =>
  //          {
  //              options.TokenValidationParameters = new TokenValidationParameters
  //              {
  //                  ValidateIssuer = true,
  //                  ValidateAudience = true,
  //                  ValidateLifetime = true,
  //                  ValidateIssuerSigningKey = true,
  //                  ValidIssuer = settings.ValidIssuer,
  //                  ValidAudience = settings.ValidAudience,
  //                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
  //              };
  //          });
  //  }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1.0", new OpenApiInfo
            {
                Title = "API документооборота",
                Version = "v1.0",
                Description = "",
                Contact = new OpenApiContact
                {
                    Name = "Name"
                }
            });

            c.ResolveConflictingActions(apiDesription => apiDesription.First());
          
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme"
            });
          
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
           // var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
           // c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

  //  public static void ConfigureControllers(this IServiceCollection services)
  //  {
  //      services.AddControllers(config =>
  //      {
  //          config.CacheProfiles.Add("30SecondsCaching", new CacheProfile
  //          {
  //              Duration = 30
  //          });
  //      });
  //  }
   
    public static void ConfigureResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }
}
