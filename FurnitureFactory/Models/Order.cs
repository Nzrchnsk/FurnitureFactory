using System;

namespace FurnitureFactory.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int KitchenId { get; set; }
        public Kitchen Kitchen { get; set; }

        public string ClientName { get; set; }
        public string ClientPhone { get; set; }

        public DateTime Date { get; set; }

        /// <summary>
        /// 0-new, 1-accepted, 2-canceled, 3-returned
        /// </summary>
        public int Status { get; set; }

        public double Sale { get; set; }
        public double Amount { get; set; }

    }
}