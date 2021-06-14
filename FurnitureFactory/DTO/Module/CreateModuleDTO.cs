using Microsoft.AspNetCore.Http;

namespace FurnitureFactory.DTO.Module
{
    public class CreateModuleDTO : ModuleDto
    {
        public IFormFile ImageFile { get; set; }
    }
}