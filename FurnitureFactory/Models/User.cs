using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace FurnitureFactory.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}