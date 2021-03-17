using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dotnet.Sample.Shared;

namespace Dotnet.Sample.Infrastructure.Languages
{
    public static class CultureInformation
    {
        public static List<Resource> GetList(string culture)
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return JsonConvert.DeserializeObject<List<Resource>>(File.ReadAllText(string.Format(CONSTANTS.LanguageLocation, CONSTANTS.Languages[0]), Encoding.UTF8), jsonSerializerSettings);
        }
    }
}