using System.Collections;
using System.Collections.Generic;

namespace FurnitureFactory.Models
{
    public class Kitchen
    {
        public int Id { get; set; }
        public double Cost { get; set; }
        
        public IEnumerable<KitchenFurnitureModule> KitchenFurnitureModules { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}