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
    
    public class CartRepository : ICartRepository
    {
        private readonly ApplictionDbContext _context;
        public CartRepository(ApplictionDbContext context)
        {
            _context = context;
            
        }
        public async Task<int> AddAsync(Cart cart)
        {

                await  _context.Carts.AddAsync(cart);
                return await _context.SaveChangesAsync();
            
        }

        public async Task<bool> ClearCartAsync(string userId)
        {

            var items = _context.Carts.Where(c => c.UserId == userId).ToList();
            _context.Carts.RemoveRange(items);
             await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Cart>> GetUserCartAsync(string UserId)
        {
            return await _context.Carts.Include(c => c.Product).Where(c => c.UserId == UserId).ToListAsync();
        }

       
    }
}
