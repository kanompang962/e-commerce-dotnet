using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderProduct;

namespace api.Dtos.Order
{
    public class OrderDtoUpdate
    {
        public bool Payment { get; set; }
        public int StatusId { get; set; }
        public OrderProductDtoUpdate[]? OrderProducts { get; set; }
    }
}