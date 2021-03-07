using Rest.Api.Domain.Languages;
using Rest.Api.Domain.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Rest.Api.Domain.Infrastructure.Languages
{
    public static class fr_fR
    {
        public static List<Resource> GetList()
        {
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            return JsonConvert.DeserializeObject<List<Resource>>(File.ReadAllText(string.Format(CONSTANTS.LanguageLocation, CONSTANTS.Languages[1]), Encoding.UTF8), jsonSerializerSettings);
        }
    }
}