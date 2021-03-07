using Rest.Api.Domain.Languages;
using Rest.Api.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Rest.Api.Domain.Infrastructure.Languages
{
    public static class en_US
    {
        public static List<Resource> GetList()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return JsonConvert.DeserializeObject<List<Resource>>(File.ReadAllText(string.Format(CONSTANTS.LanguageLocation, CONSTANTS.Languages[0])), jsonSerializerSettings);
        }
    }
}