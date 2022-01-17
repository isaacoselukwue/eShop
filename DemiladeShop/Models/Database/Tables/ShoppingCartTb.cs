using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class ShoppingCartTb
    {
        [Key]
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}