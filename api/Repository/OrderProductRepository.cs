using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

        public async Task<List<OrderProduct>> GetAllAsync()
        {
            return await _context.OrderProducts.ToListAsync();
        }
    }
}