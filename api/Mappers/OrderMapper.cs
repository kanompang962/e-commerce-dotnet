using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Models;

namespace api.Mappers
{
    public static class OrderMapper
    {
        public static OrderDto ToOrderDto(this Order orderModel)
        {
            return new OrderDto
            {
                Id = orderModel.Id,
                Date = orderModel.Date,
                TotalAmount = orderModel.TotalAmount,
                TotalPrice = orderModel.TotalPrice,
                Payment = orderModel.Payment,
                StatusId = orderModel.StatusId,
                Status = orderModel.Status?.ToOrderStatusDto(),
                AppUserId = orderModel.AppUserId,
                AppUser = orderModel.AppUser?.ToAppUserDto(),
                OrderProducts = orderModel.OrderProducts.Select(or => or.ToOrderProductDto()).ToList()
            };
        }

        public static Order ToOrderFormCreateDTO(this OrderDtoRequest productDto, string appUserId, decimal total_price, int total_amount)
        {
            return new Order
            {
                Payment = productDto.Payment,
                StatusId = productDto.StatusId,
                TotalAmount = total_amount,
                TotalPrice = total_price,
                AppUserId = appUserId,
            };
        }
    }
}