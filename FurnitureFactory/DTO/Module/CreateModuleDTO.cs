using Microsoft.AspNetCore.Http;

namespace FurnitureFactory.DTO
{
    public class CreateModuleDTO : ModuleDTO
    {
        public IFormFile ImageFile { get; set; }
    }
}