using BackendTest_Commneds.Commneds.AuthenticationCommends.Commend;
using BackendTest_Commneds.Commneds.AuthenticationCommends.CommendHandler;
using BackendTest_Commneds.Commneds.AuthenticationCommends.Query;
using BackendTest_Commneds.Commneds.AuthenticationCommends.QueryHandler;
using BackendTest_Commneds.Commneds.StudentCommends.Query;
using BackendTest_Commneds.Commneds.StudentCommends.QueryHandler;
using BackendTest_Database.DatabaseConnection;
using BackendTest_DTO.Auth;
using BackendTest_Models.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
namespace BackendTest.Util;

public static class ServiceCollectionExtension
{
    #region Base Settings
    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllers(config =>
       {
           config.CacheProfiles.Add("30SecondsCaching", new CacheProfile
           {
               Duration = 30
           });
       });
    }
    public static void ConfigureResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
    public static void ConfigureIdentity(this IServiceCollection services)
    {
        IdentityBuilder builder = services.AddIdentity<Student, IdentityRole>(o =>
        {
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireNonAlphanumeric = false;
            o.User.RequireUniqueEmail = false;
            o.SignIn.RequireConfirmedEmail = false;
        }).AddEntityFrameworkStores<BackendTestDbContext>()
        .AddDefaultTokenProviders();
    }

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
     .AddJwtBearer(options =>
     {
         options.SaveToken = true;
         options.RequireHttpsMetadata = false;
         options.Events = new JwtBearerEvents
         {
             OnChallenge = context =>
             {
                 context.Response.StatusCode = 401;
                 return Task.CompletedTask;
             }
         };
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidAudience = configuration["JwtConfig:validAudience"],
             ValidIssuer = configuration["JwtConfig:validIssuer"],
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(s: configuration["JwtConfig:secret"]!)),
             ClockSkew = TimeSpan.Zero
         };
     });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        _ = services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BackEnd Test WebAPI",
                Version = "v1",
                Description = "BackEnd Test WebAPI Services.",
                Contact = new OpenApiContact
                {
                    Name = "Mahfouz Khalil ."
                },
            });
            c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
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
                new string[] {}
            }
        });
        });
    }
    #endregion
    public static void AddCommendTransients(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<AddStudentCommend, Student>, AddStudentCommendHandler>();
        services.AddTransient<IRequestHandler<LogInCommend, TokenModel>, LogInCommendHanlder>();
        services.AddTransient<IRequestHandler<GetTokenQuery, JwtSecurityToken>, GetTokenQueryHandler>();
        services.AddTransient<IRequestHandler<GetCurrentStudentImagesQuery, List<string>>, GetCurrentStudentImagesQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllStudentsQuery, IEnumerable<Student>>, GetAllStudentsQueryHandler>();
        services.AddTransient<IRequestHandler<GetAllStudentsPortfoliosUrlsQuery, List<string>>, GetAllStudentsPortfoliosUrlsQueryHandler>();
    }
}
