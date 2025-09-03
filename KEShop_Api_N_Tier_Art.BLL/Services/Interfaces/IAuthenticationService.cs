using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserResponse> LoginAsync(LoginRequest loginRequest);
        Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest request);
        Task<string> ConfirmEmail(string token, string userId);
        Task<bool> ForgotPassword(ForgotPasswordRequest request);
        Task<bool> ResetPassword(ResetPasswordRequest request);

    }
}
