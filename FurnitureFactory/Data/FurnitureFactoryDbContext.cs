using System.Linq;
using System.Reflection;
using FurnitureFactory.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Module = FurnitureFactory.Models.Module;

namespace FurnitureFactory.Data
{
    public class FurnitureFactoryDbContext : IdentityDbContext<User, IdentityRole<int>, int,
        IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Module> FurnitureModules { get; set; }
        public DbSet<Order> Orders { get; set; }

        public FurnitureFactoryDbContext(DbContextOptions<FurnitureFactoryDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

    }
}