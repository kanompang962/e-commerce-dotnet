using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderProduct;
using api.Models;

namespace api.Dtos.Order
{
    public class OrderDtoRequest
    {
        public int TotalAmount { get; set; }
        public int TotalPrice { get; set; }
        public bool Payment { get; set; }
        public OrderProductDtoRequest[]? OrderProducts { get; set; }
    }
}