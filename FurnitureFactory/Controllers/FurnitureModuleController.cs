using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FurnitureFactory.Data;
using FurnitureFactory.DTO.Module;
using FurnitureFactory.Initializers;
using FurnitureFactory.Models;
using FurnitureFactory.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactory.Controllers
{
    [ApiController]
    [Route("api/module")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = Rolse.Admin)]
    public class FurnitureModuleController : ControllerBase
    {
        private readonly FurnitureFactoryDbContext _context;
        private readonly EfRepository<FurnitureModule> _repository;
        private readonly IMapper _mapper;

        public FurnitureModuleController(FurnitureFactoryDbContext context, EfRepository<FurnitureModule> repository,
            IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("list")]
        public ActionResult<List<ModuleDto>> GetList() =>
            _context.FurnitureModules.ToList().Select(module => new ModuleDto
                    {Id = module.Id, Name = module.Name, Price = module.Price, Image = module.Image})
                .ToList();


        [HttpPost("")]
        public async Task<ActionResult> Post([FromBody] CreateModuleDTO createModuleDto)
        {
            var fileName = createModuleDto.ImageFile.GetHashFileName();
            await FileHelper.CreateFile(new[] {"upload", "module"}, fileName, createModuleDto.ImageFile);

            var module = new FurnitureModule
            {
                Name = createModuleDto.Name,
                Price = createModuleDto.Price,
                Image = fileName
            };

            try
            {
                await _repository.AddAsync(module);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] EditModuleDTO editModuleDto, int id)
        {
            var module = await _repository.GetByIdAsync(id);
            if (module is null)
            {
                return BadRequest();
            }

            module.Name = editModuleDto.Name;
            module.Price = editModuleDto.Price;
            await _repository.UpdateAsync(module);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModuleDto>> Get(int id)
        {
            var module =  await _repository.GetByIdAsync(id);
            if (module is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ModuleDto>(module));
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete([FromRoute] int id)
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