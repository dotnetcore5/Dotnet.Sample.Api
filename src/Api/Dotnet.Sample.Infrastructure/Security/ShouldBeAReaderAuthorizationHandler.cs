using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Infrastructure.Security
{
    public class ShouldBeAReaderRequirement : IAuthorizationRequirement
    {
    }

    public class ShouldBeAReaderAuthorizationHandler : AuthorizationHandler<ShouldBeAReaderRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAReaderRequirement requirement)
        {
            //can be a custom logic for the authoriztion
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }

            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (claim.Value == Roles.Admin || claim.Value == Roles.Editor || claim.Value == Roles.Reader)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}