using System;
using System.Collections.Generic;

namespace FurnitureFactory.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CratedAt { get; set; } = DateTime.Now;

        public bool IsSale { get; set; }

        public double Sale { get; set; }
        public double TotalCast { get; set; }

        public List<Sale> Sales { get; set; }

    }
}