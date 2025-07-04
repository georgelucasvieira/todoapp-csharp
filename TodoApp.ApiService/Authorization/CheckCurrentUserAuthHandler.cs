using Microsoft.AspNetCore.Authorization;

namespace ApiService;

public static class AuthorizationHanlderExtensions
{
    public static AuthorizationBuilder AddCurrentUserAuthHandler(this AuthorizationBuilder builder)
    {
        builder.Services.AddScoped<IAuthorizationHandler, CheckCurrentUserAuthHandler>();
        return builder;
    }

    public static AuthorizationPolicyBuilder RequireCurrentUser(this AuthorizationPolicyBuilder builder)
    {
        return builder.RequireAuthenticatedUser()
                      .AddRequirements(new CheckCurrentUserAuthRequirement());
    }

    private class CheckCurrentUserAuthRequirement : IAuthorizationRequirement { }

    private class CheckCurrentUserAuthHandler(CurrentUser currentUser) : AuthorizationHandler<CheckCurrentUserAuthRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckCurrentUserAuthRequirement requirement)
        {
            if (currentUser.User is not null)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}

