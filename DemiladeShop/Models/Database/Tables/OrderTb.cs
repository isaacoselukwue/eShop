using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class OrderTb
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductAmt { get; set; }
        public string MyProperty { get; set; }
        public decimal AmountPaid { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string PaymentReference { get; set; }
        public DateTime TimePaid { get; set; }

    }
}