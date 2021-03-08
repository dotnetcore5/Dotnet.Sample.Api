using Microsoft.AspNetCore.Mvc;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Domain
{
    [ApiVersion(ApiVersionNumbers.V1)]
    [ApiVersion(ApiVersionNumbers.V2)]
    [ApiController]
    [Produces(ApiAnnotations.Produces)]
    [Consumes(ApiAnnotations.Consumes)]
    [Route(RouteNames.RouteAttribute)]
    public partial class BaseApiController : ControllerBase
    {
    }
}