using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xero.Demo.Api.Domain.Languages;
using Xero.Demo.Api.Domain.Models;

namespace Xero.Demo.Api.Domain.Infrastructure.Languages
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