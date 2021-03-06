using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class ProductTb
    {
        [Key]
        public Guid Id { get; set; }
        public string Productname { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public Guid VendorId { get; set; }
        //public byte ProductImages { get; set; }
        public Guid CategoryId { get; set; }
        public ICollection<ProductImgTb> Images { get; set; }
    }
}