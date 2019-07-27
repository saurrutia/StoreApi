using Microsoft.EntityFrameworkCore;
using Store.Core.Account;
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
            modelBuilder.Entity<ProductLikesEntity>()
                .HasKey(pl => new { pl.ProductId, pl.AccountId });

            modelBuilder.Entity<ProductLikesEntity>()
                .HasOne(pl => pl.Product)
                .WithMany(p => p.AccountLikes)
                .HasForeignKey(a => a.ProductId);

            modelBuilder.Entity<ProductLikesEntity>()
                .HasOne(pl => pl.Account)
                .WithMany(p => p.LikedProducts)
                .HasForeignKey(a => a.AccountId);

            modelBuilder.Entity<ProductEntity>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<AccountEntity>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProductEntity>()
                .HasData(ProductEntity.CreateDumpData().ToArray());

            modelBuilder.Entity<AccountEntity>()
                .HasData(AccountEntity.CreateDumpData().ToArray());

            modelBuilder.Entity<ProductLikesEntity>()
                .HasData(ProductLikesEntity.CreateDumpData().ToArray());
        }

        

        public DbSet<ProductEntity> Product { get; set; }
        public DbSet<AccountEntity> Account { get; set; }
        public DbSet<ProductLikesEntity> ProductLikes { get; set; }


    }
}
