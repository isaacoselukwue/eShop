using DemiladeShop.Models.Database.Migration;
using DemiladeShop.Models.Database.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DemiladeShop.Models.Database
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext() : base("ShopDbContext")
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ShopDbContext, Configuration>("ShopDbContext"));
        }
        public DbSet<CategoryTb> Categories { get; set; }
        public DbSet<ProductTb> Products { get; set; }
        public DbSet<OrderTb> Orders { get; set; }
    }

}