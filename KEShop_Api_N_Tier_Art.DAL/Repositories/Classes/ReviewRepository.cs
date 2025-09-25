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
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplictionDbContext _context;

        public ReviewRepository(ApplictionDbContext context)
        {
            _context = context;
        }


        public async Task<bool> HasUserReviewdProduct(string userId, int productId)
        {
           
            return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }



        public async Task AddReviewAsync(Review request, string userId)
        {
            request.UserId = userId;
            request.ReviewDate = DateTime.UtcNow;
           await _context.Reviews.AddAsync(request);
            await _context.SaveChangesAsync();
        }

       
    }
}
