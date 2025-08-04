using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class AuthenticationSerive : IAuthenticationService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationSerive(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _configuration = configuration;
           
        }

        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                throw new Exception("Invalid Email or password");
            }

            var isPassValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!isPassValid)
            {
                throw new Exception("Invalid Email or password");
            }
            return new UserResponse()
            {
               Token = await CreateTokenAsync(user),

            };

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
                    Token = registerRequest.Email,
                   
                };
            }
            else
            {
              throw new Exception($"{Result.Errors}" );
            }
        }

        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var Claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
         
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtOptions")["SecretKey"]));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims : Claims,
                expires : DateTime.Now.AddDays(15), 
                signingCredentials : credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
}
}
