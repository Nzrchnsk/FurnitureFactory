using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactory.Data;
using FurnitureFactory.DTO;
using FurnitureFactory.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactory.Controllers
{
    [ApiController]
    [Route("module")]
    // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    // [Authorize(Roles = "admin")]
    public class FurnitureModuleController : ControllerBase
    {
        private readonly FurnitureFactoryDbContext _context;

        public FurnitureModuleController(FurnitureFactoryDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("list")]
        public ActionResult<List<ModuleDTO>> GetList()
        {
            List<FurnitureModule> moduleList = _context.FurnitureModules.ToList();
            List<ModuleDTO> moduleDTOList = new List<ModuleDTO>();
            foreach (var module in moduleList)
            {
                moduleDTOList.Add(new ModuleDTO
                {
                    Id = module.Id,
                    Name = module.Name,
                    Price = module.Price,
                    Image = module.Image
                });
            }
            return Ok(moduleDTOList);
        }

        
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public ActionResult<ModuleDTO> Get([FromQuery] int id = 0)
        {
            var module = _context.FurnitureModules.FirstOrDefault(x => x.Id == id);
            if (module is null)
            {
                return BadRequest();
            }
            var moduleDTO = new ModuleDTO
            {
                Id = module.Id,
                Name = module.Name,
                Price = module.Price,
                Image = module.Image
            };
            return Ok(moduleDTO);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Post([FromBody] CreateModuleDTO createModuleDTO)
        {
            var module = new FurnitureModule
            {
                Name = createModuleDTO.Name,
                Price = createModuleDTO.Price,
                Image = createModuleDTO.Image
            };
            var addResult = await _context.AddAsync(module);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Put([FromBody] EditModuleDTO editModuleDTO, [FromRoute] int id = 0)
        {
            var module = _context.FurnitureModules.FirstOrDefault(x => x.Id == id);
            if (module is null)
            {
                return BadRequest();
            }
            module.Name = editModuleDTO.Name;
            module.Price = editModuleDTO.Price;
            var updateResult = _context.Update(module);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete([FromRoute] int id = 0)
        {
            var module = _context.FurnitureModules.FirstOrDefault(x => x.Id == id);
            if (module is null)
            {
                return BadRequest();
            }
            _context.FurnitureModules.Remove(module);
            _context.SaveChanges();
            return Ok();
        }
    }
}