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
                AppUserId = orderModel.AppUserId,
                AppUser = orderModel.AppUser?.ToAppUserDto()
            };
        }

        public static Order ToOrderFormCreateDTO(this OrderDtoRequest productDto, string appUserId)
        {
            return new Order
            {
                TotalAmount = productDto.TotalAmount,
                TotalPrice = productDto.TotalPrice,
                AppUserId = appUserId,
            };
        }
    }
}