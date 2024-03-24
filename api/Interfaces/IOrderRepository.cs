using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Order;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetByUserAsync(AppUser appUser);
        Task<Order> CreateAsync(Order order);
        Task<Order?> UpdateAsync(OrderDtoUpdate orderDto,int id);
    }
}