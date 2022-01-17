using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Dtos.Products
{
    public class ProductModel
    {
        public string Productname { get; set; }
        public byte ProductImage { get; set; }
        public string Productdescription { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public Guid VendorId { get; set; }
    }
}