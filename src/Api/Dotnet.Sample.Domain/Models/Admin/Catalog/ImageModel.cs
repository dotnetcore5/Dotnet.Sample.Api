﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Models.Admin.Catalog
{
    public class ImageModel
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public int SortOrder { get; set; }
    }
}
