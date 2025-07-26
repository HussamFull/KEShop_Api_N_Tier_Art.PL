
using KEShop_Api_N_Tier_Art.BLL.Services;
using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Scalar;
using Scalar.AspNetCore;

namespace KEShop_Api_N_Tier_Art.PL
{
    public class Program
    {
        public static void Main(string[] args)
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
            
            // Register the services with dependency injection
            // builder.Services.AddScoped<CategoryService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
