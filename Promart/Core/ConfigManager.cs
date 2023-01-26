using System;
using System.IO;
using System.Text.Json;

namespace Promart.Core
{
    public static class ConfigManager
    {
        private static string defaultConfigFile =
            @"
                {
                  ""connectionString"": ""Data Source=DESKTOP-P4JVSUH;Initial Catalog=PromartDesktop;Integrated Security=True;Trust Server Certificate=True""
                }
            ";

        public static ConfigurationFile? Open()
        {
            try
            {
                var text = File.ReadAllText("config.json");
                return JsonSerializer.Deserialize<ConfigurationFile>(text);
            }
            catch(Exception ex) 
            {
                return null;
            }
        }
    }

}