using Microsoft.EntityFrameworkCore;
using Store.Core.Product;

namespace Store.Persistence
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            :base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity
                {
                    Id = 1,
                    Name = "Potato Chips",
                    Likes = 0,
                    Price = 0,
                    Stock = 0
                });
        }

        public DbSet<ProductEntity> Products { get; set; }

       
    }
}
