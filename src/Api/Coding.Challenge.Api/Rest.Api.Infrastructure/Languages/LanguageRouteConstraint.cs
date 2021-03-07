using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Rest.Api.Domain.Languages
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey("culture"))
                return false;

            var culture = values["culture"].ToString();
            return culture.ToLowerInvariant() == "en-US".ToLowerInvariant() || culture.ToLowerInvariant() == "fr-FR".ToLowerInvariant();
        }
    }
}