using KEShop_Api_N_Tier_Art.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Scalar;
using Scalar.AspNetCore;
using KEShop_Api_N_Tier_Art.BLL.Services.Classes;
using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Classes;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.Utils;
using System.Threading.Tasks;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Identity;

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
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ISeedData, SeedData>();
            // Register Identity services
            builder.Services.AddIdentity<ApplictionUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplictionDbContext>()
                .AddDefaultTokenProviders();

            // Register the services with dependency injection
            // builder.Services.AddScoped<CategoryService>();


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


            app.MapControllers();

            app.Run();
        }
    }
}
