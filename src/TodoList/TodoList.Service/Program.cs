using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TodoList.Application.Mappers;
using TodoList.Infra.Data.PostgresSQL.Context;
using TodoList.Service;
using TodoList.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();
SerilogSettingsConfig.ConfigureLogging(builder.Configuration, builder.Environment.EnvironmentName);

builder.Services.AddDbContext<TodoListDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("TodoListConnectionStrings"));
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityServiceUrl"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "sub";
        options.SaveToken = true;
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                // Log the token
                Log.Information($"Received Token: {context.SecurityToken}");

                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
    });


builder.Services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
builder.Services.AddRepositories();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddCommands();

builder.Host.UseSerilog();

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

//Add support to logging request with SERILOG
app.UseSerilogRequestLogging();
app.Services.SeedDatabase();
app.MapControllers();
app.Run();

public partial class Program


{ } 