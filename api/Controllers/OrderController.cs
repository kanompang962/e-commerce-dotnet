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
        private readonly IProductRepository _productRepo;
        private readonly IOrderProductRepository _orderProductRepo;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(
            IOrderRepository orderRepo, 
            IOrderProductRepository orderProductRepo, 
            IProductRepository productRepo,
            UserManager<AppUser> userManager
        )
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _userManager = userManager;
            _orderProductRepo = orderProductRepo;
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
            var orderProduct = orderDto.OrderProducts;
            var total_amount = 0;
            decimal total_price = 0;

            if(appUser == null)
                return BadRequest("account does not exists");
            
            // calculator total, price
            if (orderProduct?.Length > 0 || orderProduct != null)
            {
                foreach (var item in orderProduct)
                {
                    var product = await _productRepo.GetByIdAsync(item.ProductId);
                    total_amount += item.Quantity;
                    total_price += item.Quantity * product!.Price;
                    
                }
            }
            
            // add order
            var order = orderDto.ToOrderFormCreateDTO(appUser.Id, total_price, total_amount);
            var result = await _orderRepo.CreateAsync(order);

            if(result == null)
                return StatusCode(500, "Could not create");

            // add order item
            if (orderProduct?.Length > 0 || orderProduct != null)
            {
                foreach (var item in orderProduct)
                {
                    var orderItem = item.ToOrderProductFormCreateDTO(result.Id);
                    var itemResult = await _orderProductRepo.CreateAsync(orderItem);

                    if(itemResult == null)  
                        return BadRequest("order product colud not create");
                }
            }
            
            return CreatedAtAction(
                nameof(GetById),
                new {id = result.Id},
                result.ToOrderDto()
            );
        }
    }
}