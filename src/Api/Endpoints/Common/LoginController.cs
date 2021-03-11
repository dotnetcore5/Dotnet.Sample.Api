using Microsoft.AspNetCore.Mvc;
using Xero.Demo.Api.Domain;
using Xero.Demo.Api.Domain.Extension;
using Xero.Demo.Api.Xero.Demo.Domain.Services;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Endpoints.Common
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
        /// <param name="culture">Enter the culture</param>
        /// <returns>Create jwt token for POST and DELETE request for Products</returns>
        [HttpPost(Roles.Admin)]
        public IActionResult AuthenticateAdmin(string culture = "en-US")
        {
            var response = _userService.Authenticate(Roles.Admin);

            if (response == null)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            return Ok(response);
        }

        /// <summary>
        /// Creates jwt token for GET and PUT request for Products
        /// </summary>
        /// <param name="culture">Enter the culture</param>
        /// <returns>Create jwt token for PUT request for Products</returns>
        [HttpPost(Roles.Editor)]
        public IActionResult AuthenticateEditor(string culture = "en-US")
        {
            var response = _userService.Authenticate(Roles.Editor);

            if (response == null)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            return Ok(response);
        }

        /// <summary>
        /// Creates jwt token for only GET request for Products
        /// </summary>
        /// <param name="culture">Enter the culture</param>
        /// <returns>Create jwt token for only GET request for Products</returns>
        [HttpPost(Roles.Reader)]
        public IActionResult AuthenticateReader(string culture = "en-US")
        {
            var response = _userService.Authenticate(Roles.Reader);

            if (response == null)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            return Ok(response);
        }
    }
}