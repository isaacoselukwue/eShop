using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Dtos.Orders
{
    public class OrderModel
    {
        public Guid UserId { get; set; }
        public string PaymentId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
    public class OrderPut
    {
        public bool IsOrderCompleted { get; set; }
    }
}