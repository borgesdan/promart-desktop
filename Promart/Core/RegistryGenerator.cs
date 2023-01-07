using System;

namespace Promart.Core
{
    public class RegistryGenerator
    {
        public static string? NewRegistry(string fullName)
        {
            DateTime dateTime = DateTime.Now;

            string firstChars = fullName.Substring(0, 3);
            string year = dateTime.Year.ToString();
            string[] guid = Guid.NewGuid().ToString().Split('-');

            return $"{firstChars.ToUpperInvariant()}-{year}-{guid[1].ToUpperInvariant()}";
        }
    }
}
