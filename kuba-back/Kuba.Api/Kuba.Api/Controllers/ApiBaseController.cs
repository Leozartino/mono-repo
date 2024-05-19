using Microsoft.AspNetCore.Mvc;

namespace Kuba.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiBaseController : ControllerBase
    {
    }
}
