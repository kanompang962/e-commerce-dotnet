using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
         private readonly ApplicationDBContext _context;
        public OrderStatusRepository( ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<bool> OrderStatusExists(int id)
        {
            return await _context.OrderStatuses.AnyAsync(s => s.Id == id);
        }
    }
}