using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class ProductImgTb
    {
        [Key]
        public Guid PhotoID { get; set; }
        public byte[] ImageBytes { get; set; }
        public string ProductId { get; set; }
        public ProductTb Product { get; set; }
    }
}