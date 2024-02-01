using System.Reflection;
using IdentityServer.Service;
using IdentityServer.Service.Data;
using IdentityServer.Service.Extensions;
using IdentityServer.Service.Models;
using IdentityService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Polly;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();
SerilogSettingsConfig.ConfigureLogging(builder.Configuration, builder.Environment.EnvironmentName);

//builder.Services.AddDbContext<ApplicationDbContext>(opt =>
//{
//    opt.UseNpgsql(builder.Configuration.GetConnectionString("IdentityProviderConnectionStrings"));
//});
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder
        .ConfigureServices()
        .ConfigurePipeline();

var retryPolicy = Policy
        .Handle<NpgsqlException>()
        .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(10));

    retryPolicy.ExecuteAndCapture(() => SeedData.EnsureSeedData(app));

app.Run();

public partial class Program


{ } 