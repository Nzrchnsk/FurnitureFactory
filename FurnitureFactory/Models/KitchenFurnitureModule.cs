namespace FurnitureFactory.Models
{
    public class KitchenFurnitureModule
    {
        public int Id { get; set; }

        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }

        public int FurnitureModuleId { get; set; }
        public FurnitureModule FurnitureModule { get; set; }
    }
}