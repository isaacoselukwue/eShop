using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database.Tables
{
    public class VendorTb
    {
        public Guid Id { get; set; }
        public string VendorName { get; set; }
        public string VendorCompany { get; set; }
        public string MyProperty { get; set; }
    }
}