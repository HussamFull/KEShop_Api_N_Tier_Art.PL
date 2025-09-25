using KEShop_Api_N_Tier_Art.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        
        Task<bool> HasUserReviewdProduct(string userId, int productId);

        Task AddReviewAsync(Review review, string userId);
    }
}
