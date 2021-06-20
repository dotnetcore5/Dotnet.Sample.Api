using Dotnet.Sample.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.Models.Catalog
{
    public class ProductManufacturerMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ManufacturerId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
    }
}
