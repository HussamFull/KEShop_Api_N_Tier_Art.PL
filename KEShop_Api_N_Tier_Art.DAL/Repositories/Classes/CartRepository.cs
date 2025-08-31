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
        public int Add(Cart cart)
        {

            _context.Carts.Add(cart);
                return _context.SaveChanges();
            
        }

        public List<Cart> GetUserCart(string UserId)
        {
            return _context.Carts.Include(c => c.Product).Where(c => c.UserId == UserId).ToList();
        }
    }
}
