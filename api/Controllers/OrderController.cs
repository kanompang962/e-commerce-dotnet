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
        private readonly IOrderStatusRepository _orderStatusRepo;
        private readonly IOrderProductRepository _orderProductRepo;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(
            IOrderRepository orderRepo, 
            IOrderProductRepository orderProductRepo, 
            IProductRepository productRepo,
            IOrderStatusRepository orderStatusRepo,
            UserManager<AppUser> userManager
        )
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _userManager = userManager;
            _orderProductRepo = orderProductRepo;
            _orderStatusRepo = orderStatusRepo;
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
            var order = await _orderRepo.GetByIdAsync(id);
            if(order == null)
                return NotFound("order does not exists");
            
            return Ok(order.ToOrderDto());
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetByUser()
        {
            // check user
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            if(appUser == null)
                return BadRequest("token does not exists");

            var order = await _orderRepo.GetByUserAsync(appUser);
            if(order == null)
                return NotFound("order does not exists");
            
            return Ok(order.Select(o => o.ToOrderDto()));
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDtoRequest orderDto)
        {
            // check user
            var username = User.GetUsername();
            if(username == null)
                return BadRequest("account does not exists");

            var appUser = await _userManager.FindByNameAsync(username);
            if(appUser == null)
                return BadRequest("account does not exists");

            // check order status
            var orderStatus = await _orderStatusRepo.OrderStatusExists(orderDto.StatusId);
            if(!orderStatus)
                return BadRequest("order status does not exits");

            var orderProduct = orderDto.OrderProducts;
            var total_amount = 0;
            decimal total_price = 0;

            // calculator total, price
            if (orderProduct?.Length > 0 || orderProduct != null)
            {
                foreach (var item in orderProduct)
                {
                    var checkProductId = await _productRepo.ProductExists(item.ProductId);
                    if (!checkProductId)
                        return BadRequest("product does not exists");

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
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromBody] OrderDtoUpdate orderDto, int id)
        {
            // update order product
            var orderProduct = orderDto.OrderProducts;
            foreach (var item in orderProduct!)
            {
                var orderProductResult = await _orderProductRepo.UpdateAsync(
                    id,
                    item.ProductId,
                    item.Quantity
                );

                if(orderProductResult == null)
                    return NotFound("order product does not exists");
            }

            // update order
            var order = await _orderRepo.UpdateAsync(orderDto, id);
            if(order == null)
                return NotFound("order does not exists");

            return Ok(order.ToOrderDto());
        }
    }
}