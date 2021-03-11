using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Xero.Demo.Api.Domain.Models;

namespace Xero.Demo.Api.Domain.Languages
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey("culture"))
            {
                return false;
            }
            var culture = values["culture"].ToString();
            return culture.ToLowerInvariant() == CONSTANTS.Languages[0].ToLowerInvariant() || culture.ToLowerInvariant() == CONSTANTS.Languages[1].ToLowerInvariant();
        }
    }
}