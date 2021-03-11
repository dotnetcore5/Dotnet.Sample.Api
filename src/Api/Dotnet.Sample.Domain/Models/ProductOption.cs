using System;
using System.Text.Json.Serialization;

namespace Dotnet.Sample.Domain.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore] public bool IsNew { get; }
    }
}