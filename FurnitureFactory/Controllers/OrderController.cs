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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FurnitureFactory.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly FurnitureFactoryDbContext _context;
        private readonly IAsyncRepository<Order> _repository;
        private readonly IAsyncRepository<Sale> _saleRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;


        public OrderController(FurnitureFactoryDbContext context, IAsyncRepository<Order> repository,
            IMapper mapper, UserManager<User> userManager, IAsyncRepository<Sale> saleRepository)
        {
            _saleRepository = saleRepository;
            _userManager = userManager;
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Order>>> GetList()
        {
            if (User.IsInRole(Rolse.Admin))
            {
                return _context.Orders.Include(u => u.User)
                    .Include(o => o.Sales)
                    .ThenInclude(o => o.Module)
                    .ToList();
            }

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            return _context.Orders.Include(u => u.User).Include(o => o.Sales)
                .ThenInclude(o => o.Module).Where(o => o.UserId == user.Id).ToList();
        }

        [HttpPost("")]
        public async Task<ActionResult> Post([FromBody] List<Module> request)
        {
            if (!request.Any()) return Ok();
            var user = await _context.Users.Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            var totalSale = user.Orders.Select(o => o.TotalCast)?.Sum();
            var sale = 0.0;
            if (totalSale != null)
            {
                sale = totalSale switch
                {
                    <5000 => 0,
                    >100000 => 40,
                    >50000 => 30,
                    >25000 => 15,
                    >10000 => 5,
                    >5000 => 2.5,
                    _ => sale
                };
            }

            var totalOrderCost = request.Select(o => o.Price).Sum();

            var totalOrderCostWithDiscount = totalOrderCost - totalOrderCost / 100 * sale;

            var order = new Order()
            {
                UserId = user.Id,
                Sale = sale,
                TotalCast = totalOrderCostWithDiscount,
                IsSale = false,
            };

            try
            {
                order = await _repository.AddAsync(order);
                if (order is null) return BadRequest();
                var sales = request.Select(module => new Sale() {ModuleId = module.Id, OrderId = order.Id}).ToList();
                await _saleRepository.AddAsyncRange(sales);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Ok();
        }

        [HttpPut("sale/{id:int}")]
        public async Task<ActionResult> Put(int id)
        {
            var order = await _repository.GetByIdAsync(id);
            if (order is null)
            {
                return BadRequest();
            }

            order.IsSale = true;

            await _repository.UpdateAsync(order);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> Get(int id)
        {
            var order = await _context.Orders.Include(o => o.Sales).ThenInclude(s => s.Module)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (order is null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var module = await _repository.GetByIdAsync(id);
            if (module is null)
            {
                return BadRequest();
            }

            await _repository.DeleteAsync(module);

            return Ok();
        }

        [HttpGet("history/{userId:int?}")]
        [Authorize(Roles = Rolse.Admin)]
        public async Task<List<Order>> HistoryForAdmin(int userId)
        {
            return await _context.Orders.Include(o => o.User)
                .Include(o => o.Sales)
                .ThenInclude(s => s.Module)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        [HttpGet("history")]
        public async Task<List<Order>> History()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            return await _context.Orders.Include(o => o.User)
                .Include(o => o.Sales)
                .ThenInclude(s => s.Module)
                .Where(o => o.UserId == user.Id)
                .ToListAsync();
        }
    }
}