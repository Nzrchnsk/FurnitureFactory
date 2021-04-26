using System.Collections;
using System.Collections.Generic;

namespace FurnitureFactory.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }

        public IEnumerable<KitchenModule> KitchenModules { get; set; }
    }
}