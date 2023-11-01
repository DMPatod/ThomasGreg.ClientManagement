using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/error")]
        public IActionResult HandleError()
        {
            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return new ObjectResult(new
            {
                Message = "An error occurred while processing request",
                Error = ex is not null ? ex.Error.Message : null
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
