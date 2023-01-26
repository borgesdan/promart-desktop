using System.Text.Json.Serialization;

namespace Promart.Core
{
    public class ConfigurationFile
    {
        [JsonPropertyName("connectionString")]
        public string? ConnectionString { get; set; }
    }
}
