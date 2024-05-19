using Kuba.Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Kuba.Api.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ApiBaseController
    {
        public IActionResult Error(int statusCode)
        {
            return new ObjectResult(new ApiResponse<ErrorsController>(statusCode, ""));
        }
    }
}
