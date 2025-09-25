using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Interfaces
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewRequest reviewRequest, string userId);
    }
}
