using Microsoft.AspNetCore.Mvc;
using MetricsApi.Models;

namespace MetricsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MetricsController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostMetric([FromBody] Metric input)
        {
            return Ok(new { message = $"Metric '{input.Name}' received with value {input.Value}" });
        }
    }
}