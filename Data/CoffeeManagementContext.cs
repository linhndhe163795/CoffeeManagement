using ManagementCoffee.Model;
using Microsoft.EntityFrameworkCore;

namespace ManagementCoffee.Data
{
    public class CoffeeManagementContext : DbContext
    {
        public CoffeeManagementContext(DbContextOptions<CoffeeManagementContext> options) : base(options) 
        { 

        }
        public DbSet<User> User { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Products>  Products { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItems>()
                        .HasKey(o => new { o.OrderID, o.productCode});
            modelBuilder.Entity<Orders>()
                        .HasKey(o => new { o.OrderID});
            modelBuilder.Entity<Products>().HasKey(o => new { o.productCode });
            modelBuilder.Entity<Categories>().HasKey(o => new { o.ID });
            modelBuilder.Entity<User>().HasKey(o => new { o.Id });
        }
    }
}
