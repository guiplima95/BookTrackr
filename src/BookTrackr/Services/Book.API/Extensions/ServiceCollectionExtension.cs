using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Book.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<BookContext>(options =>
            {
                options.UseSqlServer(
                    configuration["ConnectionStrings:SqlServer"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });
    }

    public static IHostApplicationBuilder AddCustomCors(this IHostApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((_) => true)
                    .AllowCredentials();
            });
        });

        return builder;
    }
}