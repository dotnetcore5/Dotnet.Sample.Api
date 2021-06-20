using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Models.Admin.Sale
{
    public class OrderListModel
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime OrderPlacementDateTime { get; set; }
        public decimal TotalOrderPrice { get; set; }
    }
}
