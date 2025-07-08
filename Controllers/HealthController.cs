using Microsoft.AspNetCore.Mvc;
using MetricsApi.Models;
using System.Text.Json;

namespace MetricsApi.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        // GET /health/status
        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            return Ok(new { status = "API is running" });
        }

        // GET /health/status/version
        [HttpGet("status/version")]
        public IActionResult GetVersion()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "version.json");

                if (!System.IO.File.Exists(path))
                {
                    return NotFound(new { error = "version.json not found" });
                }

                var json = System.IO.File.ReadAllText(path);
                var versionInfo = JsonSerializer.Deserialize<VersionInfo>(json);

                if (versionInfo == null)
                {
                    return StatusCode(500, new { error = "Failed to parse version.json" });
                }

                return Ok(versionInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Could not read version.json",
                    details = ex.Message
                });
            }
        }
    }
}
