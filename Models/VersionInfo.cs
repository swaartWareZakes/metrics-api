using System.Text.Json;

namespace MetricsApi.Models
{
    public class VersionInfo
    {
        public string Version { get; set; } = "unknown";
        public string Environment { get; set; } = "unknown";

        public static VersionInfo LoadFromFile()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "version.json");
                if (!File.Exists(path))
                    return new VersionInfo();

                var json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<VersionInfo>(json) ?? new VersionInfo();
            }
            catch
            {
                return new VersionInfo();
            }
        }
    }
}
