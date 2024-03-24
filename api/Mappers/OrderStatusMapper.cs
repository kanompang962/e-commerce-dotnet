using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Models;

namespace api.Mappers
{
    public static class OrderStatusMapper
    {
         public static OrderStatusDto ToOrderStatusDto (this OrderStatus orderStatus)
        {
            return new OrderStatusDto
            {
                Id = orderStatus.Id,
                Name = orderStatus.Name,
            };
        }
    }
}