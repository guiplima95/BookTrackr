using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Builder;
using Book.API.Application.Extensions;
using Book.API.Converters;
using Book.API.Endpoints.OpenApi;
using Book.API.Extensions;
using Book.API.Infrastructure;
using Book.API.Infrastructure.Extensions;
using Book.API.Infrastructure.Persistence;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();

builder.Services.AddCustomDbContext(builder.Configuration);

builder.AddCustomCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.WriteIndented = true;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    options.SerializerOptions.Converters.Add(new TimeSpanConverter());
    options.SerializerOptions.Converters.Add(new DateTimeOffsetConverter());
});

// Add Serilog:
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DI:
builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

// Add GlobalExceptionHandler:
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// Add Versioning:
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

// Add Endpoints:
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

// Add Configuration OpenApi with Swagger:
builder.Services.ConfigureOptions<ConfigureSwaggerGenOptions>();

WebApplication app = builder.Build();

ApiVersionSet apiVersionSet = app.NewApiVersionSet()
    .HasApiVersion(new ApiVersion(1))
    .ReportApiVersions()
    .Build();

RouteGroupBuilder versionedGroup = app
    .MapGroup("api/v{version:apiVersion}")
    .WithApiVersionSet(apiVersionSet);

app.MapEndpoints(versionedGroup);

app.MigrateDbContext<BookContext>((context, services) =>
{
    IWebHostEnvironment env = services.GetRequiredService<IWebHostEnvironment>();
    ILogger<BookContextSeed> logger = services.GetRequiredService<ILogger<BookContextSeed>>();

    new BookContextSeed()
        .SeedAsync(context, env, logger)
        .Wait();
});

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

        foreach (ApiVersionDescription description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();

            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseHttpsRedirection();

// Map HealthChecks
app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseStaticFiles();

app.UseAuthorization();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

await app.RunAsync();