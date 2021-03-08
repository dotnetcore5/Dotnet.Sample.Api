using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Domain.Security
{
    public class ShouldBeAnAdminRequirement : IAuthorizationRequirement
    {
    }

    public class ShouldBeAnAdminRequirementHandler : AuthorizationHandler<ShouldBeAnAdminRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ShouldBeAnAdminRequirement requirement)
        {
            //can be a custom logic for the authorization
            if (!context.User.HasClaim(x => x.Type == ClaimTypes.Role)) return Task.CompletedTask;

            var claim = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (claim.Value == Roles.Admin || claim.Value == Roles.Editor || claim.Value == Roles.Reader) context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}