using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Classes
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplictionDbContext _context;

        public OrderItemRepository(ApplictionDbContext context)
        {
            _context = context;
        }

        public async Task AddRangeAsync(List<OrderItem> items)
        {
            await _context.OrderItems.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

        
    }
}
