namespace FurnitureFactory.Models
{
    public class KitchenModule
    {
        public int Id { get; set; }

        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}