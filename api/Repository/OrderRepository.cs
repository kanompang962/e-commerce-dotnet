using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Order;
using api.Dtos.OrderProduct;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IOrderProductRepository _orderProduct;
        public OrderRepository(ApplicationDBContext context, IOrderProductRepository orderProduct)
        {
            _context = context;
            _orderProduct = orderProduct;
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
                        .Include(a => a.AppUser)
                        .Include(s => s.Status) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก OrderPStatus
                        .ToListAsync();
            // return await _context.Orders.Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(op => op.OrderProducts) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก OrderProduct
                .ThenInclude(op => op.Product) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Product
                .ThenInclude(c => c!.Category) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก Category
                .Include(a => a.AppUser)
                .Include(s => s.Status) // เพิ่มบรรทัดนี้เพื่อดึงข้อมูลจาก OrderPStatus
                .FirstOrDefaultAsync(o => o.Id == id);

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
        public async Task<Order?> UpdateAsync(OrderDtoUpdate orderDto, int id)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if(existingOrder == null)
                return null;

            // update total amount,price
            var order= await GetByIdAsync(id);
            var totalAmount = 0;
            decimal totalPrice = 0;
            foreach (var item in order!.OrderProducts)
            {
                totalPrice += item.Quantity * item.Product!.Price;
                totalAmount += item.Quantity;
            }

            existingOrder.TotalPrice = totalPrice; 
            existingOrder.TotalAmount = totalAmount; 
            existingOrder.Payment = orderDto.Payment;
            existingOrder.StatusId = orderDto.StatusId;

            await _context.SaveChangesAsync();

            return existingOrder;
        }
    }
}