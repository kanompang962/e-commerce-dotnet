using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderProductRepository
    {
        Task<List<OrderProduct>> GetAllAsync();
        Task<OrderProduct> CreateAsync(OrderProduct orderProduct);
    }
}