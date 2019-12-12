using ASP.NETCoreAuthWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreAuthWebApi.DataAcces
{
    public class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Cart { get; set; }
    }
}
