using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Data
{
    public class ApplictionDbContext : DbContext 
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Brand> Brands { get; set; }

       

        public ApplictionDbContext(DbContextOptions<ApplictionDbContext> options) : base(options)
        {
        }
     
       
    }
}
