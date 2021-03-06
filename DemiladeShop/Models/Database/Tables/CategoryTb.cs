using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class CategoryTb
    {
        [Key]
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public byte CategoryImage { get; set; }
    }
}