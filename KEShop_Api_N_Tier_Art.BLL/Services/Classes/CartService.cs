using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository ) 
        {
            _cartRepository = cartRepository;
            
        }

        public bool AddToCart(CartRequest request, string UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddToCartAsync(CartRequest request, string UserId)
        {
            var newItem = new Cart
                { ProductId = request.ProductId,
                UserId = UserId,
                 Count = 1 }
            ;
            return await _cartRepository.AddAsync(newItem) > 0;
        }

    

        public async Task<CartSummaryResponse> GetCartSummaryResponseAsync(string UserId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(UserId);
            var response = new CartSummaryResponse
            {
                Items = cartItems.Select(ci => new CartResponse
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price,
                    Count = ci.Count,
                    MainImage = ci.Product.MainImage,
                    ProductDescription = ci.Product.Description

                }).ToList(),
            };
            return response;
        }

      
    }
}
