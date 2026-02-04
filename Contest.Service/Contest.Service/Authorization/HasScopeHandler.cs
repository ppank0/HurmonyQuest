using Microsoft.AspNetCore.Authorization;

namespace ContestService.API.Authorization;

public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      HasScopeRequirement requirement
    )
    {
        var scopeClaim = context.User
        .FindFirst(c => c.Type == "scope" && c.Issuer == requirement.Issuer);

        // If user does not have the scope claim or it's empty, get out of here
        if (scopeClaim == null || string.IsNullOrWhiteSpace(scopeClaim.Value))
            return Task.CompletedTask;

        var scopes = scopeClaim.Value.Split(' ');

        // Succeed if the scope array contains the required scope
        if (scopes.Any(s => s == requirement.Scope))
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
