using Microsoft.AspNetCore.Mvc;

namespace ClientManagement.API.Controllers
{
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("api/errors")]
        public IActionResult HandleError()
        {
            return Problem();
        }
    }
}
