using Microsoft.AspNetCore.Mvc;

namespace MetricsApi.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class HealthController : ControllerBase
        {
            [HttpGet("status")]
            public IActionResult GetStatus()
            {
                return Ok(new { status = "API is running" });
            }
        }
}
