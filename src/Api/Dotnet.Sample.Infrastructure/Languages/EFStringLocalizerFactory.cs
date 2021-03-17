using Dotnet.Sample.Shared;
using Microsoft.Extensions.Localization;
using System;

namespace Dotnet.Sample.Infrastructure.Languages
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
                    Name = CONSTANTS.Languages[0],
                    Resources = CultureInformation.GetList(CONSTANTS.Languages[0])
                },
                new Culture
                {
                    Name = CONSTANTS.Languages[1],
                    Resources = CultureInformation.GetList(CONSTANTS.Languages[1])
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