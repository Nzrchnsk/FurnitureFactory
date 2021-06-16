namespace FurnitureFactory.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
        
    }
}