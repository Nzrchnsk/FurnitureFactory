using FurnitureFactory.Data;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactory.Controllers
{
    [ApiController]
    [Route("api/modules")]
    public class ModuleController : ControllerBase
    {
        private readonly FurnitureFactoryDbContext _context;
        public  ModuleController(FurnitureFactoryDbContext context)
        {
            _context = context;
        }

    }
}