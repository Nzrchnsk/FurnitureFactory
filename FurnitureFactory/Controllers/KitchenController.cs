using FurnitureFactory.Data;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactory.Controllers
{
    [ApiController]
    [Route("kitchen")]
    public class KitchenController : ControllerBase
    {
        private readonly FurnitureFactoryDbContext _context;
        public KitchenController(FurnitureFactoryDbContext context)
        {
            _context = context;
        }
        // GET
    }
}