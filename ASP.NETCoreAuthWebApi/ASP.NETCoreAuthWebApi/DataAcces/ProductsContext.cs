using ASP.NETCoreAuthWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreAuthWebApi.DataAcces
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Name ="BANANA",
                    Description = "yellow",
                    Coast = 10,
                },
                new Product
                {
                    Name = "APPLE",
                    Description = "Red",
                    Coast = 5
                }
                );
        }
    }
}
