using System.Linq;
using System.Threading.Tasks;
using FurnitureFactory.Data;
using FurnitureFactory.Models;
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
            if (!_сontext.FurnitureModules.Any())
            {
                _сontext.FurnitureModules.Add( new Module()
                {
                    Name = "Стол",
                    Price = 100.0,
                    Description ="Кухонный"
                });
                _сontext.FurnitureModules.Add(new Module()
                {
                    Name = "Ручка",
                    Description= "Металлическая",
                    Price = 200.0
                });
                await _сontext.SaveChangesAsync();
            }
            await _сontext.Database.MigrateAsync();
        }
    }
}