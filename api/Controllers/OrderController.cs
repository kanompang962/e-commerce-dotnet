using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Interfaces;
using api.Mappers;
using api.Models;
using app_dotnet.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepo;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(IOrderRepository orderRepo, UserManager<AppUser> userManager)
        {
            _orderRepo = orderRepo;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var order = await _orderRepo.GetAllAsync();
            var orderDto = order.Select(o => o.ToOrderDto());
            return Ok(orderDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // check user
            var order = await _orderRepo.GetByIdAsync(id);
            if(order == null)
                return NotFound("order does not exists");
            
            return Ok(order.ToOrderDto());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDtoRequest orderDto)
        {
            // check user
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if(appUser == null)
                return BadRequest("account does not exists");
            
            var order = orderDto.ToOrderFormCreateDTO(appUser.Id);
            await _orderRepo.CreateAsync(order);

             return Ok("success");
        }
    }
}