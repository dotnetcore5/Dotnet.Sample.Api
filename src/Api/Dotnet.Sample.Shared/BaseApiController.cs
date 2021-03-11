using Microsoft.AspNetCore.Mvc;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Shared
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