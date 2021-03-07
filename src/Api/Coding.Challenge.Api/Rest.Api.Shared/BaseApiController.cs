using Microsoft.AspNetCore.Mvc;

namespace Rest.Api.Domain
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/{culture:culture}/v{version:apiVersion}/[controller]")]
    public partial class BaseApiController : ControllerBase
    {
    }
}