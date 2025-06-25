using Microsoft.EntityFrameworkCore;

namespace BethenyPieShop.Models
{
    public class BethenysPieShopDbContext : DbContext
    {
        public BethenysPieShopDbContext(DbContextOptions<BethenysPieShopDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }

        
    }
}
