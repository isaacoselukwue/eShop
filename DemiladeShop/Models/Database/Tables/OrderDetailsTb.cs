using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class OrderDetailsTb
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int Qty { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        public Guid OrderId { get; set; }
        public OrderTb Order { get; set; }
        public Guid ProductId { get; set; }
        public ProductTb Product { get; set; }
    }
}