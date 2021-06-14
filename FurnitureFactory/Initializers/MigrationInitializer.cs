using System.Threading.Tasks;
using FurnitureFactory.Data;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.Initializers
{
    public class MigrationInitializer
    {
        
        private readonly FurnitureFactoryDbContext _сontext;

        public MigrationInitializer(FurnitureFactoryDbContext сontext)
        {
            _сontext = сontext;
        }
        public async Task Run()
        {
            await _сontext.Database.MigrateAsync();
        }
    }
}