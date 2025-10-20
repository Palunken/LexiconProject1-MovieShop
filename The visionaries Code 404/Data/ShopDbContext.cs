using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace The_visionaries_Code_404.Data
{
    public class ShopDbContext: DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderRow> OrderRows { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public ShopDbContext()
        {
               
        }

        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }
    }
}
