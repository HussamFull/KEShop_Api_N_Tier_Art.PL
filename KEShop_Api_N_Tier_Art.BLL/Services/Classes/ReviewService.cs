using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository, IReviewRepository reviewRepository)
        {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> AddReviewAsync(ReviewRequest reviewRequest, string userId)
        {
            // Check if the user has an approved order for the product
            var hasOrder = await _orderRepository.UserHasApprovedOrderForProductAsync(userId, reviewRequest.ProductId);
            if (!hasOrder)
            {
                return false; // User cannot review the product
            }
          
            var alreadyReviews = await _reviewRepository.HasUserReviewdProduct(userId, reviewRequest.ProductId);
            if (alreadyReviews)
            {
                return false; // User has already reviewed the product
            }
            var review = reviewRequest.Adapt<Review>();

            await _reviewRepository.AddReviewAsync(review, userId);
            return true;
        }


    }
}
