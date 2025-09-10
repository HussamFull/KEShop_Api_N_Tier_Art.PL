using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
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
using ForgotPasswordRequest = KEShop_Api_N_Tier_Art.DAL.DTO.Requests.ForgotPasswordRequest;
using LoginRequest = KEShop_Api_N_Tier_Art.DAL.DTO.Requests.LoginRequest;
using RegisterRequest = KEShop_Api_N_Tier_Art.DAL.DTO.Requests.RegisterRequest;
using ResetPasswordRequest = KEShop_Api_N_Tier_Art.DAL.DTO.Requests.ResetPasswordRequest;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class AuthenticationSerive : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticationSerive(UserManager<ApplicationUser> userManager, 
            IConfiguration configuration,
             IEmailSender emailSender,
             SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _signInManager = signInManager;
        }

        //public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        //{
            
        //    var user = await _userManager.FindByEmailAsync(loginRequest.Email);
        //    if (user is null)
        //    {
        //        throw new Exception("Invalid Email or password");
        //    }

        //  var result =   await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
        //    if (!result.Succeeded)
        //    {
        //        return new UserResponse()
        //        {
        //            Token = await CreateTokenAsync(user),

        //        };

        //    }
        //    else if (result.IsLockedOut)
        //    {
        //        throw new Exception("User is locked out");
        //    }

        //    else if (result.IsNotAllowed)
        //    {
        //        throw new Exception("Please Confirm your Email");
        //    }
           
        //    else
        //    {
        //        throw new Exception("Invalid Email or password");
        //    }

        //}

        public async Task<UserResponse> LoginAsync(LoginRequest loginRequest)
        {
            // البحث عن المستخدم باستخدام البريد الإلكتروني
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                throw new Exception("Invalid Email or password");
            }

            // التحقق من صحة كلمة المرور
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);

            // التحقق من نتيجة تسجيل الدخول
            if (result.Succeeded)
            {
                // إذا نجحت العملية، قم بإنشاء وإرجاع التوكن
                return new UserResponse()
                {
                    Token = await CreateTokenAsync(user),
                };
            }
            else if (result.IsLockedOut)
            {
                // إذا كان المستخدم مقفولاً
                throw new Exception("User is locked out");
            }
            else if (result.IsNotAllowed)
            {
                // إذا كان المستخدم غير مسموح له بالدخول (مثل عدم تأكيد البريد الإلكتروني)
                throw new Exception("Please Confirm your Email");
            }
            else
            {
                // إذا فشلت العملية لأي سبب آخر (مثل كلمة مرور خاطئة)
                throw new Exception("Invalid Email or password");
            }
        }

        public async Task<string> ConfirmEmail(string token , string userId)
            {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("User not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email confirmed successfully";
            }
            return "Email confirmation failed";
            
        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest registerRequest, HttpRequest request)
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
                // Send Email Confirmation
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapeToken = Uri.EscapeDataString(token);
                var emailUrl = $"{request.Scheme}://{request.Host}/api/identity/account/confirmEmail?token={escapeToken}&userId={user.Id}";

                await _userManager.AddToRoleAsync(user, "Customer");
                await _emailSender.SendEmailAsync(
                   user.Email,
                    "Confirm your Email",
                    $"<h1>Welcome {user.FullName}</h1>" +
                    $"<p>Please confirm your email by clicking the link below:</p>" +
                    $"<a href='{emailUrl}'> Confirm your email  </a>"
                );
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

        public async Task<bool> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
          
            var random = new Random();
            var code = random.Next(1000, 9999).ToString(); 


            user.CodeResetPassword = code;
            user.PasswordResetCodeExpiry = DateTime.Now.AddMinutes(15);


            await _userManager.UpdateAsync(user);

            // Send email with reset password link
           
            await _emailSender.SendEmailAsync(
                request.Email,
                "Reset Password",
                $"<h1>Reset your password</h1>" +
                $"<p>code is  to reset your password:  {code} </p>"
                
            );
            return true;
        }

        public async Task<bool> ResetPassword(ResetPasswordRequest request)
            {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.CodeResetPassword != request.Code || user.PasswordResetCodeExpiry < DateTime.Now)
            {
                throw new Exception("Invalid or expired reset code");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (result.Succeeded)
            
                    await _emailSender.SendEmailAsync(request.Email , "change password", 
                        "<h1> your password is changed </h1> ");
            return true;

        }
}
}
