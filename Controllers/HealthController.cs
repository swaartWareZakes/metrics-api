using Microsoft.AspNetCore.Mvc;
using MetricsApi.Models;
using System.Text.Json;

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

        [HttpGet("status/version")]
        public IActionResult GetVersion()
        {
            try
            {
                var json = System.IO.File.ReadAllText("version.json");
                var versionInfo = JsonSerializer.Deserialize<VersionInfo>(json);
                return Ok(versionInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Could not read version.json", details = ex.Message });
            }
        }
    }
}
