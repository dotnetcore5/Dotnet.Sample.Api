using Microsoft.AspNetCore.Mvc;
using Xero.Demo.Api.Domain;
using Xero.Demo.Api.Domain.Extension;
using Xero.Demo.Api.Xero.Demo.Domain.Services;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Endpoints.Common
{
    [ApiVersion(ApiVersionNumbers.V1)]
    [ApiVersion(ApiVersionNumbers.V2)]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(string culture = "en-US")
        {
            var response = _userService.Authenticate();

            if (response == null) return BadRequest(ModelState.GetErrorMessages());

            return Ok(response);
        }
    }
}