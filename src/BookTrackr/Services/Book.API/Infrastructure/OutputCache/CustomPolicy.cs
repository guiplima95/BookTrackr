using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.Primitives;

namespace Book.API.Infrastructure.OutputCache;

internal sealed class CustomPolicy : IOutputCachePolicy
{
    public CustomPolicy()
    {
    }

    /// <inheritdoc />
    ValueTask IOutputCachePolicy.CacheRequestAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        bool attemptOutputCaching = AttemptOutputCaching(context);
        context.EnableOutputCaching = true;
        context.AllowCacheLookup = attemptOutputCaching;
        context.AllowCacheStorage = attemptOutputCaching;
        context.AllowLocking = true;

        // Vary by any query by default
        context.CacheVaryByRules.QueryKeys = "*";

        return ValueTask.CompletedTask;
    }

    /// <inheritdoc />
    ValueTask IOutputCachePolicy.ServeFromCacheAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        return ValueTask.CompletedTask;
    }

    /// <inheritdoc />
    ValueTask IOutputCachePolicy.ServeResponseAsync(OutputCacheContext context, CancellationToken cancellation)
    {
        HttpResponse response = context.HttpContext.Response;

        // Verify existence of cookie headers
        if (!StringValues.IsNullOrEmpty(response.Headers.SetCookie))
        {
            context.AllowCacheStorage = false;
            return ValueTask.CompletedTask;
        }

        // Check response code
        if (response.StatusCode != StatusCodes.Status200OK)
        {
            context.AllowCacheStorage = false;
            return ValueTask.CompletedTask;
        }

        return ValueTask.CompletedTask;
    }

    private static bool AttemptOutputCaching(OutputCacheContext context)
    {
        // Check if the current request fulfills the requirements to be cached

        HttpRequest request = context.HttpContext.Request;

        // Verify the method
        if (!HttpMethods.IsGet(request.Method) && !HttpMethods.IsHead(request.Method))
        {
            return false;
        }

        // Verify existence of authorization headers
        //if (!StringValues.IsNullOrEmpty(request.Headers.Authorization) ||
        //    request.HttpContext.User?.Identity?.IsAuthenticated == true)
        //{
        //    return false;
        //}

        return true;
    }
}