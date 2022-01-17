using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class OrderTb
    {
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public decimal OrderTotal { get; set; }
        public bool IsOrderCompleted { get; set; }
        public Guid VendorId { get; set; }
        public Guid UserId { get; set; }
        public decimal AmountPaid { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }
        public string PaymentReference { get; set; }
        public DateTime TimePaid { get; set; }
        public ICollection<OrderDetailsTb> OrderDetails { get; set; }

    }
}