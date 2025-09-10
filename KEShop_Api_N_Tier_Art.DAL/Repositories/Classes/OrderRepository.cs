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
    }
}
