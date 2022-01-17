using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Dtos.Categories
{
    public class CategoryModel
    {
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public byte CategoryImage { get; set; }
    }
}