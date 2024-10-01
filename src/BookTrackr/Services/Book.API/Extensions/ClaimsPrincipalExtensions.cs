using System.Security.Claims;

namespace Book.API.Extensions;

internal static class ClaimsPrincipalExtensions
{
    internal static Guid UserId(this ClaimsPrincipal claimsPrincipal)
    {
        return Guid.Parse(claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}