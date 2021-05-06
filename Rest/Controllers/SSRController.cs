using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Rest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SSRController : ControllerBase
    {
        private readonly ILogger<SSRController> _logger;

        public SSRController(ILogger<SSRController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<string> ListServers()
        {
            return "yuyi";
        }
    }
}