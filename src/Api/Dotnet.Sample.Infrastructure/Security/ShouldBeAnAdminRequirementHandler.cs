using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Infrastructure.Security
{
    public class ShouldBeAnAdminRequirement : IAuthorizationRequirement
    {
    }

    public class ShouldBeAnAdminRequirementHandler : AuthorizationHandler<ShouldBeAnAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAnAdminRequirement requirement)
        {
            //can be a custom logic for the authorization
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }

            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (claim.Value == Roles.Admin)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}