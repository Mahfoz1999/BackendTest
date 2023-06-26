using BackendTest.FluentValidation;
using BackendTest.Middleware;
using BackendTest.Util;
using BackendTest_Database.DatabaseConnection;
using BackendTest_Services.AuthenticationService;
using BackendTest_Services.StudentService;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
Assembly assembly = Assembly.GetExecutingAssembly();
builder.Services.AddControllers();
builder.Services.TryAddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.TryAddScoped<IStudentService, StudentService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
builder.Services.AddTransient<Mediator>();
builder.Services.AddCommendTransients();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StudentFormDtoValidator>());
builder.Services.AddHttpClient();
builder.Services
   .AddDbContext<BackendTestDbContext>(options =>
   {
       string? connectionString = builder.Configuration.GetConnectionString("DefaultSQL");
       options.UseSqlServer(connectionString);
   });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromHours(1));
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.ConfigureControllers();
var app = builder.Build();
app.UseCors(cors => cors
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetIsOriginAllowed(origin => true)
              .AllowCredentials());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var env = app.Environment;
string path = Path.Combine(env.WebRootPath, "Portfolios").ToLower();
if (!Directory.Exists(path))
    Directory.CreateDirectory(path);
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();

