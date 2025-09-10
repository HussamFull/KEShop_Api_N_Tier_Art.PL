using KEShop_Api_N_Tier_Art.BLL.Services.Classes;
using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL;
using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Classes;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.Utils;
using KEShop_Api_N_Tier_Art.PL.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scalar;
using Scalar.AspNetCore;
using Stripe;
using System.Text;
using System.Threading.Tasks;
using FileService = KEShop_Api_N_Tier_Art.BLL.Services.Classes.FileService;
using ProductService = KEShop_Api_N_Tier_Art.BLL.Services.Classes.ProductService;


namespace KEShop_Api_N_Tier_Art.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();


            // ******* /// 


            // Register the DbContext with dependency injection
            builder.Services.AddDbContext<ApplictionDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            // Register the repositories with dependency injection
            
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddScoped<ICheckOutService, CheckOutService>();
          

            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<ICartService, CartService>();

            builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();


            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddScoped<IFileService, FileService>();


            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IBrandService, BrandService>();


            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationSerive>();
            builder.Services.AddScoped<IEmailSender, EmailSetting>();
            // Register Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                    //options.Password.RequireDigit = true;
                    //options.Password.RequireLowercase = true;
                    //options.Password.RequireUppercase = true;
                    //options.Password.RequireNonAlphanumeric = false;
                    //options.Password.RequiredLength = 8;
                    //options.User.RequireUniqueEmail = true;
                    //options.SignIn.RequireConfirmedEmail = true;
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                    //options.Lockout.MaxFailedAccessAttempts = 10;
                   // options.Lockout.AllowedForNewUsers = true;
                }

                )
                .AddEntityFrameworkStores<ApplictionDbContext>()
                .AddDefaultTokenProviders();
            // JWT 
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                // ValidIssuer = builder.Configuration["Jwt:Issuer"],
                // ValidAudience = builder.Configuration["Jwt:Audience"],
                // zQgXs8zbGqzjqGu6V9D21YKTIMHy0HiB
                //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("zQgXs8zbGqzjqGu6V9D21YKTIMHy0HiB"))

                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwtOptions")["SecretKey"]))
            };
        });

            // Register the services with dependency injection
            // builder.Services.AddScoped<CategoryService>();


            // Configure Stripe settings
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
            StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];


            // ******* ///

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            var scope = app.Services.CreateScope();
            var objectOfSeedData =  scope.ServiceProvider.GetRequiredService<ISeedData>();
           await objectOfSeedData.DataSeedingAsync();
            await objectOfSeedData.IdentityDataSeedingAsync();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();


            app.MapControllers();

            app.Run();
        }
    }
}
