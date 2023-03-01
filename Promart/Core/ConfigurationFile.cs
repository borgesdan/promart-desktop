using System.Text.Json.Serialization;

namespace Promart.Core
{
    public class ConfigurationFile
    {
        [JsonPropertyName("connectionStrings")]
        public ConnectionString ConnectionStrings { get; set; } = new ConnectionString();

        public class ConnectionString
        {
            [JsonPropertyName("default")]
            public string Default { get; set; } = string.Empty;

            [JsonPropertyName("noInitialCalog")]
            public string NoInitialCatalog { get; set; } = string.Empty;
        }
    }
    
}
