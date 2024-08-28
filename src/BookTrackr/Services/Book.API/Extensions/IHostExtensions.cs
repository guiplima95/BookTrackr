using Book.API.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Book.API.Extensions;

public static class IHostExtensions
{
    public static IWebHost MigrateDbContext<TContext>(
        this IWebHost host,
        Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(host);

        using IServiceScope scope = host.Services.CreateScope();

        MigrateDbContext(scope, seeder);

        return host;
    }

    public static IHost MigrateDbContext<TContext>(this IHost host) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(host);

        host.MigrateDbContext<TContext>((_, __) => { });

        return host;
    }

    public static IHost MigrateDbContext<TContext>(
        this IHost host,
        Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(host);

        using IServiceScope scope = host.Services.CreateScope();

        MigrateDbContext(scope, seeder);

        return host;
    }

    private static void MigrateDbContext<TContext>(
        IServiceScope scope,
        Action<TContext, IServiceProvider> seeder) where TContext : DbContext
    {
        IServiceProvider services = scope.ServiceProvider;
        ILogger<TContext> logger = services.GetRequiredService<ILogger<TContext>>();
        TContext context = services.GetRequiredService<TContext>();

        try
        {
            logger.LogDbContextMigrating<TContext>();

            var retry = Policy.Handle<SqlException>()
                 .WaitAndRetry(
                 [
                         TimeSpan.FromSeconds(3),
                         TimeSpan.FromSeconds(5),
                         TimeSpan.FromSeconds(8),
                 ]);

            //if the sql server container is not created on run docker compose this
            //migration can't fail for network related exception. The retry options for DbContext only
            //apply to transient exceptions
            retry.Execute(() => InvokeSeeder(seeder, context, services));
        }
        catch (SqlException ex)
        {
            logger.LogDbContextMigratingException<TContext>(ex);
        }
    }

    private static void InvokeSeeder<TContext>(
        Action<TContext, IServiceProvider> seeder,
        TContext context,
        IServiceProvider services) where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}