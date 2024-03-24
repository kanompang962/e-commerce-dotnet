using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Dtos.OrderProduct;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/order_product")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductRepository _orderProductRepo;
        private readonly IOrderRepository _orderRepo;
        public OrderProductController( IOrderProductRepository orderProductRepo, IOrderRepository orderRepo)
        {
            _orderProductRepo = orderProductRepo;
            _orderRepo = orderRepo;
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
        //     var orderProducts = await _orderProductRepo.GetAllAsync();
        //     return Ok(orderProducts); 
        // }

        [HttpDelete("{orderId:int}")]
        public async Task<IActionResult> Delete([FromBody] OrderProductDtoRequest orderProduct, [FromRoute] int orderId)
        {
            // delete order product
            var orderProducts = await _orderProductRepo.DeleteAsync(orderProduct,orderId);
            if(orderProducts == null)
                return BadRequest("can't not delete");
            
            // update order
            var order = await _orderRepo.GetByIdAsync(orderId);
            if(orderProducts == null)
                return NotFound("order does not exists");

            var orderProductDtos = order?.OrderProducts
            .Select(op => new OrderProductDtoUpdate
            {
                ProductId = op.ProductId,
                Quantity = op.Quantity
            })
            .ToArray();

            var existingOrder = new OrderDtoUpdate
            {
                Payment = order!.Payment,
                StatusId = order.StatusId,
                OrderProducts = orderProductDtos
            };
            var result_order = await _orderRepo.UpdateAsync(existingOrder, orderId);

            return Ok(orderProducts); 
        }

        // [HttpPut("{orderId:int}")]
        // public async Task<IActionResult> Update([FromBody] OrderProductDtoUpdate orderProductDto, [FromRoute] int orderId)
        // {
        //     var orderProducts = await _orderProductRepo.UpdateAsync(orderProductDto, orderId);
        //     if(orderProducts == null)
        //         return BadRequest("order product does not exists");

        //     return Ok(orderProducts.ToOrderProductDto()); 
        // }

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