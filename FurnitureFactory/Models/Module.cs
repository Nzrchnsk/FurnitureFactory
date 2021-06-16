using System.Collections.Generic;

namespace FurnitureFactory.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }

        public List<Order> Orders { get; set; }
    }
}