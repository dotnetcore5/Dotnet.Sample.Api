using Dotnet.Sample.Domain.Services;
using Dotnet.Sample.Infrastructure.Extensions;
using Dotnet.Sample.Shared;
using Microsoft.AspNetCore.Mvc;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.Common
{
    [ApiVersion(ApiVersionNumbers.V1)]
    [ApiVersion(ApiVersionNumbers.V2)]
    public class LoginController : BaseApiController
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates jwt token for all [GET POST PUT DELETE] request for Products
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="culture">Enter the culture</param>
        /// <returns>Create jwt token for POST and DELETE request for Products</returns>
        [HttpPost(Roles.Admin)]
        public IActionResult AuthenticateAdmin(string username = Roles.Admin, string password = "Password", string culture = "en-US")
        {
            return Authenticate(username, password);
        }

        /// <summary>
        /// Creates jwt token for GET and PUT request for Products
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="culture">Enter the culture</param>
        /// <returns>Create jwt token for PUT request for Products</returns>
        [HttpPost(Roles.Editor)]
        public IActionResult AuthenticateEditor(string username = Roles.Editor, string password = "Password", string culture = "en-US")
        {
            return Authenticate(username, password);
        }

        /// <summary>
        /// Creates jwt token for only GET request for Products
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="culture">Enter the culture</param>
        /// <returns>Create jwt token for only GET request for Products</returns>
        [HttpPost(Roles.Reader)]
        public IActionResult AuthenticateReader(string username = Roles.Reader, string password = "Password", string culture = "en-US")
        {
            return Authenticate(username, password);
        }

        private IActionResult Authenticate(string username, string password)
        {
            var response = _userService.Authenticate(username, password);

            if (response == null)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            return Ok(response);
        }
    }
}