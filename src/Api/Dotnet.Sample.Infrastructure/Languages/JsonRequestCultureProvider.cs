using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dotnet.Sample.Infrastructure.Languages
{
    public class JsonRequestCultureProvider : RequestCultureProvider
    {
        private static readonly Regex LocalePattern = new Regex(@"^[a-z]{2}(-[a-z]{2,4})?$", RegexOptions.IgnoreCase);

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            var parts = httpContext.Request.Path.Value.Split('/');
            if (parts.Length < 3)
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            if (!LocalePattern.IsMatch(parts[2]))
            {
                return Task.FromResult<ProviderCultureResult>(null);
            }

            var culture = parts[2];

            culture ??= "en-US";

            return Task.FromResult(new ProviderCultureResult(culture));
        }
    }
}