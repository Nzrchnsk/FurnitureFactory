using System.Linq;
using System.Reflection;
using FurnitureFactory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.Data
{
    public class FurnitureFactoryDbContext : IdentityDbContext<User, IdentityRole<int>, int,
        IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<KitchenFurnitureModule> KitchenFurnitureModules { get; set; }
        public DbSet<FurnitureModule> FurnitureModules { get; set; }
        public DbSet<Order> Orders { get; set; }

        public FurnitureFactoryDbContext(DbContextOptions<FurnitureFactoryDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
            if (!FurnitureModules.Any())
            {
                FurnitureModules.Add(new FurnitureModule()
                {
                    Name = "TestName1",
                    Price = 100.0
                });
                FurnitureModules.Add(new FurnitureModule()
                {
                    Name = "TestName2",
                    Price = 200.0
                });
                SaveChanges();
            }
        }

    }
}