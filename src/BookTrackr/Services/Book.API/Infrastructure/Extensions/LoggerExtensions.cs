using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Book.API.Infrastructure.Extensions;

public static class LoggerExtensions
{
    private static readonly Action<ILogger, string, string, string, int, int, Exception> _exceptionOnRetries;
    private static readonly Action<ILogger, string, string, Exception> _exceptions;
    private static readonly Action<ILogger, string, Exception?> _dbContextMigrating;
    private static readonly Action<ILogger, string, Exception> _dbContextMigratingException;

    static LoggerExtensions()
    {
        _exceptionOnRetries = LoggerMessage.Define<string, string, string, int, int>(
            logLevel: LogLevel.Warning,
            eventId: new EventId(id: 1, name: "Client.API"),
            formatString: "[{Prefix}] Exception {ExceptionType} with message {Message} detected on attempt {Retry} of {Retries}");

        _exceptions = LoggerMessage.Define<string, string>(
            logLevel: LogLevel.Error,
            eventId: new EventId(1, "Book.API"),
            formatString: "It occurred an error with message: {Message}. Expcetion: \"{Exception}\"");

        _dbContextMigrating = LoggerMessage.Define<string>(
            logLevel: LogLevel.Information,
            eventId: new EventId(2, "Book.API"),
            formatString: "Migrating database associated with context {DbContextName}");

        _dbContextMigratingException = LoggerMessage.Define<string>(
            logLevel: LogLevel.Error,
            eventId: new EventId(3, "Book.API"),
            formatString: "An error occurred while migrating the database used on context {DbContextName}");
    }

    public static void LogExceptionOnRetry(this ILogger logger, Exception exception, string prefix, int retry, int retries)
    {
        _exceptionOnRetries(logger, prefix, exception.GetType().Name, exception.Message, retry, retries, exception);
    }

    public static void LogException(this ILogger logger, Exception exception)
    {
        _exceptions(logger, exception.Message, exception.ToStringDemystified().Trim(), exception);
    }

    public static void LogDbContextMigrating<TContext>(this ILogger logger) where TContext : DbContext
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            _dbContextMigrating(logger, typeof(TContext).Name, null);
        }
    }

    public static void LogDbContextMigratingException<TContext>(this ILogger logger, Exception exception)
        where TContext : DbContext
    {
        _dbContextMigratingException(logger, typeof(TContext).Name, exception);
    }
}