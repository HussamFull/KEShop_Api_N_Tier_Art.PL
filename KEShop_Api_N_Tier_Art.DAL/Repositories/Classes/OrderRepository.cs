using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Classes
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplictionDbContext _context;

        public OrderRepository( ApplictionDbContext context)
        {
            _context = context;
        }

        public async Task<Order?> AddAsync(Order order)
        {
            
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetUserByOrderAsync(int orderId)
        {
            return await _context.Orders.Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<List<Order>> GetAllWithUserAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Order>> GetByStatusAsync(OrderStatusEnum status)
        {
            return await _context.Orders
                .Where(o => o.Status == status)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }

        public async Task<List<Order>> GetOrderByUserAsync(string userId)
        {
            return await _context.Orders.Include(o => o.User)
                .OrderByDescending(o => o.OrderDate).ToListAsync();
        }

        public async Task<bool> ChangeStatusAsync(int orderId, OrderStatusEnum newStatus)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                return false; // Order not found
            }
            order.Status = newStatus;
            
           var result =  await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UserHasApprovedOrderForProductAsync(string userId, int productId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .AnyAsync(e => e.UserId == userId &&
                               e.Status == OrderStatusEnum.Approved &&
                               e.OrderItems.Any(oi => oi.ProductId == productId));
        }
    }
}
