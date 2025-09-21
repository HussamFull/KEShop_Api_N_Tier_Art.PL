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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private ApplictionDbContext _context;

        public ProductRepository(ApplictionDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DecreaseQuantityAsync(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product is null)
            {
                throw new Exception("Product not found");
            }
            if (product.Quantity < quantity)
            {
                throw new Exception("not enough stock available");
            }
            product.Quantity -= quantity;
            await _context.SaveChangesAsync();
        }

        public async Task DecreaseQuantityAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i => i.productId).ToList();

            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();

            foreach (var product in products)
            {
                var item = items.First(i => i.productId == product.Id);
                
                if (product.Quantity < item.quantity)
                {
                    throw new Exception($"not enough stock available ");
                }
                product.Quantity -= item.quantity;
            }
            await _context.SaveChangesAsync();
        }
    }
}
