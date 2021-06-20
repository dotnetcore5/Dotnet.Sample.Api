using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Models.Admin.Catalog
{
    public class ProductSpecificationModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int SortOrder { get; set; }
    }
}
