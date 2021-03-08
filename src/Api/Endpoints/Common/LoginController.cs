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

        [HttpPost(Roles.Admin)]
        public IActionResult AuthenticateAdmin(string culture = "en-US")
        {
            var response = _userService.Authenticate(Roles.Admin);

            if (response == null) return BadRequest(ModelState.GetErrorMessages());

            return Ok(response);
        }

        [HttpPost(Roles.Editor)]
        public IActionResult AuthenticateEditor(string culture = "en-US")
        {
            var response = _userService.Authenticate(Roles.Editor);

            if (response == null) return BadRequest(ModelState.GetErrorMessages());

            return Ok(response);
        }

        [HttpPost(Roles.Reader)]
        public IActionResult AuthenticateReader(string culture = "en-US")
        {
            var response = _userService.Authenticate(Roles.Reader);

            if (response == null) return BadRequest(ModelState.GetErrorMessages());

            return Ok(response);
        }
    }
}