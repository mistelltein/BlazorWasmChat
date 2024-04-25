using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorWasmChat.Authentication
{
    internal static class IdentityComponentEndpointsRouteBuilderExtension
    {
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoint)
        {
            var accountGroup = endpoint.MapGroup("/Account");
            accountGroup.MapPost("Logout", async (ClaimsPrincipal user, SignInManager<AppUser> signInManager) =>
            {
                await signInManager.SignOutAsync();

                return TypedResults.LocalRedirect("/");
            });

            return accountGroup;
        }
    }
}