using Rest.Api.Domain.Infrastructure.Languages;
using Microsoft.Extensions.Localization;
using System;

namespace Rest.Api.Domain.Languages
{
    public class EFStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly LocalizationDbContext _db;

        public EFStringLocalizerFactory()
        {
            _db = new LocalizationDbContext();
            _db.AddRange(
                new Culture
                {
                    Name = "en-US",
                    Resources = en_US.GetList()
                },
                new Culture
                {
                    Name = "fr-FR",
                    Resources = fr_fR.GetList()
                }
            );
            _db.SaveChanges();
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new EFStringLocalizer(_db);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new EFStringLocalizer(_db);
        }
    }
}