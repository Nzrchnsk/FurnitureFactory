using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FurnitureFactory.Data;
using FurnitureFactory.DTO.Module;
using FurnitureFactory.Initializers;
using FurnitureFactory.Interfaces;
using FurnitureFactory.Models;
using FurnitureFactory.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureFactory.Controllers
{
    [ApiController]
    [Route("api/modules")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ModuleController : ControllerBase
    {
        private readonly FurnitureFactoryDbContext _context;
        private readonly IAsyncRepository<Module> _repository;
        private readonly IMapper _mapper;

        public ModuleController(FurnitureFactoryDbContext context, IAsyncRepository<Module> repository,
            IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public ActionResult<List<Module>> GetList() => _context.FurnitureModules.ToList();

        [HttpPost("")]
        [Authorize(Roles = Rolse.Admin)]
        public async Task<ActionResult> Post([FromForm] CreateModuleDTO createModuleDto)
        {
            var fileName = createModuleDto.Photo.GetHashFileName();
            await FileHelper.CreateFile(new[] {"upload", "module"}, fileName, createModuleDto.Photo);

            var module = new Module
            {
                Name = createModuleDto.Name,
                Price = createModuleDto.Price,
                Photo = fileName,
                Description = createModuleDto.Description
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
        [Authorize(Roles = Rolse.Admin)]
        public async Task<ActionResult> Put([FromForm] EditModuleDTO editModuleDto, int id)
        {
            var module = await _repository.GetByIdAsync(id);
            if (module is null)
            {
                return BadRequest();
            }

            if (editModuleDto.Photo is not null)
            {
                var fileName = editModuleDto.Photo.GetHashFileName();
                if (module.Photo is not null)
                    await FileHelper.DeleteFile(new[] {"upload", "module"}, module.Photo);
                await FileHelper.CreateFile(new[] {"upload", "module"}, fileName, editModuleDto.Photo);
                module.Photo = fileName;
            }

            module.Name = editModuleDto.Name;
            module.Price = editModuleDto.Price;
            module.Description = editModuleDto.Description;
            await _repository.UpdateAsync(module);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModuleDto>> Get(int id)
        {
            var module = await _repository.GetByIdAsync(id);
            if (module is null)
            {
                return NotFound();
            }

            module.Photo = $"{Request.Scheme}://{Request.Host}/img/{module.Photo}";
            return Ok(module);
        }

        [Authorize(Roles = Rolse.Admin)]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var module = _context.FurnitureModules.FirstOrDefault(x => x.Id == id);
            if (module is null)
            {
                return BadRequest();
            }
            await FileHelper.DeleteFile(new[] {"upload", "module"}, module.Photo);


            _context.FurnitureModules.Remove(module);
            _context.SaveChanges();
            return Ok();
        }
    }
}