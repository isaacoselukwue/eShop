using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Dtos.ShoppingCart
{
    public class ShoppingCartModel
    {
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
    }
}