using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Optmni.DAL.Model;
using Optmni.DAL.ModelConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optmni.DAL
{
    public class OptmniDbContext : IdentityDbContext<ApplicationUser>
    {
        public OptmniDbContext(DbContextOptions<OptmniDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Lkp_Region> Lkp_Regions { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new Lkp_RegionConfiguration());
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderDetailsConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
        }
    }
}
