using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderProduct;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/order_product")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductRepository _orderProductRepo;
        public OrderProductController( IOrderProductRepository orderProductRepo)
        {
            _orderProductRepo = orderProductRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orderProducts = await _orderProductRepo.GetAllAsync();
            return Ok(orderProducts); 
        }

        // [HttpPost]
        // public async Task<IActionResult> Create()
        // {
        //     var orderProduct = new OrderProduct {
        //         OrderId = 1,
        //         ProductId = 1
        //     };
        //     var result = await _orderProductRepo.CreateAsync(orderProduct);
        //     if(result == null)
        //         return BadRequest("could not create");

        //     return Ok(orderProduct); 
        // }
    }
}