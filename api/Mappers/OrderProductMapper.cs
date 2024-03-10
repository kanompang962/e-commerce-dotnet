using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderProduct;
using api.Models;

namespace api.Mappers
{
    public static class OrderProductMapper
    {
        public static OrderProductDto ToOrderProductDto(this OrderProduct orderProduct)
        {
            return new OrderProductDto
            {
                OrderId = orderProduct.OrderId,
                ProductId = orderProduct.ProductId,
                Quantity = orderProduct.Quantity,
                Products = orderProduct.Product?.ToProducDto()
            };
        }

        public static OrderProduct ToOrderProductFormCreateDTO(this OrderProductDtoRequest orderProductDto, int orderId)
        {
            return new OrderProduct
            {
                OrderId = orderId,
                ProductId = orderProductDto.ProductId,
                Quantity = orderProductDto.Quantity,
            };
        }
    }
}