using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int TotalAmount { get; set; }
        public int TotalPrice { get; set; }
        public string? AppUserId { get; set; }
        public AppUserDto? AppUser { get; set; }
    }
}