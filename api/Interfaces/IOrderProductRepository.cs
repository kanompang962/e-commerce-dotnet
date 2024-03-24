using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.OrderProduct;
using api.Models;

namespace api.Interfaces
{
    public interface IOrderProductRepository
    {
        Task<List<OrderProduct>> GetAllAsync();
        Task<OrderProduct> CreateAsync(OrderProduct orderProduct);
        Task<OrderProduct?> UpdateAsync(int OrderId, int ProductId, int Quantity);
        Task<OrderProduct?> DeleteAsync(OrderProductDtoRequest orderProduct,int OrderId);
    }
}