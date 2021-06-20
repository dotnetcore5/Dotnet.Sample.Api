using Dotnet.Sample.Api.Domain.Models.Sale;
using Dotnet.Sample.Api.Domain.ViewModels.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.ViewModels
{
    public class OrderModel
    {
        public Guid UserId { get; set; }
        public string OrderNumber { get; set; }
        public Guid BillingAddressId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderPlacedDateTime { get; set; }
        public DateTime OrderCompletedDateTime { get; set; }
        public decimal TotalOrderPrice { get; set; }

        public CustomerModel BillingAdddress { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
