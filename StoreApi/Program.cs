using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Store.Core.Account;
using Store.Core.Product;
using Store.Persistence;

namespace StoreApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var serviceScope = host.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<StoreDbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Account.AddRange(AccountEntity.CreateDumpData());
                context.Product.AddRange(ProductEntity.CreateDumpData());
                context.ProductLikes.AddRange(ProductLikesEntity.CreateDumpData());
                context.SaveChanges();
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
