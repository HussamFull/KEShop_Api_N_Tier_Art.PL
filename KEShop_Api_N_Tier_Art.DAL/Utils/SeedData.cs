﻿using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Utils
{
    public class SeedData : ISeedData
    {
        private readonly ApplictionDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplictionUser> _userManager;

        public SeedData(
            ApplictionDbContext context, 
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplictionUser> userManager
            ) 
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            if ( (await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
           
            if (!await _context.Categories.AnyAsync())
            {
                await _context.Categories.AddRangeAsync(
                    new Category {  Name = "Electronics" },
                    new Category {  Name = "Books" },
                    new Category {  Name = "Clothing" }
                );
            }
            if (!await _context.Brands.AnyAsync())
            {
                await _context.Brands.AddRangeAsync(
                    new Brand { Name = "BrandA" },
                    new Brand { Name = "BrandB" },
                    new Brand {  Name = "BrandC" }
                );
            }
            await _context.SaveChangesAsync();
        }

        public async Task IdentityDataSeedingAsync()
        {
            if (!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
               // await _roleManager.CreateAsync(new IdentityRole("Perent"));
            }

           
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplictionUser()
                {

                    Email = "hosam813@gmail.com",
                    UserName = "hosam813",
                    FullName = "Hosam Alnabelsi",
                    PhoneNumber = "1234567890"

                };
                var user2 = new ApplictionUser()
                {

                    Email = "hamada813@gmail.com",
                    UserName = "hamada",
                    FullName = "hamada Alnabelsi",
                    PhoneNumber = "1234567830"

                };
                var user3 = new ApplictionUser()
                {

                    Email = "mohammad@gmail.com",
                    UserName = "mohammad",
                    FullName = "mohammad Alnabelsi",
                    PhoneNumber = "1234567840"

                };

                await _userManager.CreateAsync(user1, "Pass@1212");
                await _userManager.CreateAsync(user2, "Pass@1212");
                await _userManager.CreateAsync(user3, "Pass@1212");

                await _userManager.AddToRoleAsync(user1, "Admin");
                await _userManager.AddToRoleAsync(user2, "SuperAdmin");
                await _userManager.AddToRoleAsync(user3, "Customer");


            }

            await _context.SaveChangesAsync();

        }

        
    }
}
