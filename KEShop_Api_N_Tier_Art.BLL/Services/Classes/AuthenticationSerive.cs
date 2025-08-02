using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class AuthenticationSerive : IAuthenticationService 
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthenticationSerive(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException("Login functionality is not implemented yet.");

        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = registerRequest.FullName,
                Email = registerRequest.Email,
                PhoneNumber = registerRequest.PhoneNumber,
                UserName = registerRequest.UserName,
            };

          var Result =  await _userManager.CreateAsync(user, registerRequest.Password);
        
            if (Result.Succeeded)
            {
                return new UserResponse()
                {
                    Email = registerRequest.Email,
                   
                };
            }
            else
            {
              throw new Exception($"{Result.Errors}" );
            }
        }
    }
}
