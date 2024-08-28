using Book.API;
using Book.API.Converters;
using Book.API.Extensions;
using Book.API.Infrastructure.Persistence;
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

// TODO Serilog configuration:

var app = builder.Build();

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Book.API V1");
        c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

var summaries = new[]
{
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

app.MapGet("/weatherforecast", (HttpContext httpContext) =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        })
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

await app.RunAsync();