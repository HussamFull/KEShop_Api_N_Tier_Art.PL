using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Interfaces
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(CartRequest request, string UserId);

        Task<CartSummaryResponse> GetCartSummaryResponseAsync(string UserId);
    }
}
