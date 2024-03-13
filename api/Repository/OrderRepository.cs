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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        public OrderRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                        .Include(op => op.OrderProducts) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก OrderProduct
                        .ThenInclude(op => op.Product) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Product
                        .ThenInclude(c => c!.Category) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Category
                        .Include(a => a.AppUser).ToListAsync();
            // return await _context.Orders.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(op => op.OrderProducts) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก OrderProduct
                .ThenInclude(op => op.Product) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Product
                .ThenInclude(c => c!.Category) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Category
                .Include(a => a.AppUser).FirstOrDefaultAsync(o => o.Id == id);

            if(order == null)
                return null;
            
            return order;
        }

        public async Task<List<Order>> GetByUserAsync(AppUser appUser)
        {
            var order = await _context.Orders
                .Include(op => op.OrderProducts) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก OrderProduct
                .ThenInclude(op => op.Product) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Product
                .ThenInclude(c => c!.Category) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Category
                .Include(a => a.AppUser)
                .Where(o => o.AppUserId == appUser.Id).ToListAsync();

            return order;
        }
    }
}