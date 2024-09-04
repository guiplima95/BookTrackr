using Book.API.Application.Abstractions.Data;
using Book.API.Domain.Abstractions;
using Book.API.Domain.Repositories;
using Book.API.Domain.Repositories.Users;
using Book.API.Infrastructure.Persistence;
using Book.API.Infrastructure.Persistence.Repositories;
using Book.API.Infrastructure.Time;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Book.API.Infrastructure.Extensions;

public static class InfrastructureDependecyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMediatR(config =>
            config.RegisterServicesFromAssembly(typeof(InfrastructureDependecyInjection).Assembly));

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        string? connectionString = configuration.GetConnectionString("SqlServer");

        ArgumentNullException.ThrowIfNull(connectionString, nameof(connectionString));

        string redisConnectionString = configuration.GetConnectionString("Redis")!;

        // Dapper with DbConnectionFactory: SqlConnection
        services.AddScoped<IDbConnectionFactory>(_ =>
            new DbConnectionFactory(new SqlConnection(connectionString)));

        // Redis:
        services.AddStackExchangeRedisCache(options =>
           options.Configuration = redisConnectionString);

        // DB Context:
        services.AddDbContext<BookContext>(
            (sp, options) => options.UseSqlServer(connectionString));

        // HealthChecks:
        services.AddHealthChecks()
            .AddSqlServer(connectionString)
            .AddRedis(redisConnectionString)
            .AddDbContextCheck<BookContext>();

        // Repositories:
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
    }
}