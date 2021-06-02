using System.Collections;
using System.Collections.Generic;

namespace FurnitureFactory.Models
{
    public class FurnitureModule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public IEnumerable<KitchenFurnitureModule> KitchenModules { get; set; }
    }
}