using System.Linq;
using FurnitureFactory.Models;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.Data
{
    public class FurnitureFactoryDbContext : DbContext
    {
        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<KitchenModule> kitchenModules { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Order> Orders { get; set; }

        public FurnitureFactoryDbContext(DbContextOptions<FurnitureFactoryDbContext> dbContextOptions): base(dbContextOptions)
        {
            Database.EnsureCreated();
            if (!Modules.Any())
            {
                Modules.Add(new Module()
                {
                    Name = "TestName1",
                    Price = 100.0
                });
                Modules.Add(new Module()
                {
                    Name = "TestName2",
                    Price = 200.0
                });
                SaveChanges();
            }
        }
        
    }
}