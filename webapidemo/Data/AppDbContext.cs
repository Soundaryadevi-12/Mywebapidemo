using demowebapi.Models;
using Microsoft.EntityFrameworkCore;

namespace Mywebapidemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> ProductTable { get; set; }

        public DbSet<Category> CategoryTable{ get; set; }

    }
}
