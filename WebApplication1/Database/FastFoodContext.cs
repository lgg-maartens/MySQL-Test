using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Database
{
  public class FastFoodContext : DbContext
  {
    public FastFoodContext(DbContextOptions<FastFoodContext> options) : base(options)
    {
    }

    public DbSet<Product> Product { get; set; }
    public DbSet<Klant> Klant { get; set; }    
  }
}