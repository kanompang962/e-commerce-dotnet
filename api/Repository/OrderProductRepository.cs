using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.OrderProduct;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OrderProductRepository : IOrderProductRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<OrderProduct> CreateAsync(OrderProduct orderProduct)
        {
            await _context.OrderProducts.AddAsync(orderProduct);
            await _context.SaveChangesAsync();
            return orderProduct;
        }

        public async Task<OrderProduct?> DeleteAsync(OrderProductDtoRequest orderProductDto, int OrderId)
        {
            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync(op => op.OrderId == OrderId && op.ProductId == orderProductDto.ProductId);
            if(orderProduct == null)
                return null;
            
            _context.OrderProducts.Remove(orderProduct);
            await _context.SaveChangesAsync();
            return orderProduct;
        }

        public async Task<List<OrderProduct>> GetAllAsync()
        {
            return await _context.OrderProducts.ToListAsync();
        }
        public async Task<OrderProduct?> UpdateAsync(int OrderId, int ProductId, int Quantity)
        {
            var existingOrderProduct = await _context.OrderProducts.FirstOrDefaultAsync(op => 
                op.OrderId ==  OrderId&& 
                op.ProductId == ProductId
            );

            if(existingOrderProduct == null)
                return null;

            existingOrderProduct.Quantity = Quantity;
            await _context.SaveChangesAsync();

            return existingOrderProduct;
        }
    }
}