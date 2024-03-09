using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Order
{
    public class OrderDtoRequest
    {
        public int TotalAmount { get; set; }
        public int TotalPrice { get; set; }
        // public string? AppUserId { get; set; }
    }
}