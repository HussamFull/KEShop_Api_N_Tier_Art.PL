using KEShop_Api_N_Tier_Art.DAL.Data;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories
{

    // This class is a placeholder for the Category repository. هون بنعلمل كرد للكاتيجوري 
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplictionDbContext dbContext;

        public CategoryRepository(ApplictionDbContext context)
        {
            dbContext = context;
        }

        public int Add(Category category)
        {
         
            dbContext.Categories.Add(category);
            return dbContext.SaveChanges(); 

        }

        public IEnumerable<Category> GetAll(bool withTracking = false)
        {
            if (withTracking)
         
            return dbContext.Categories.ToList();

            return dbContext.Categories.AsNoTracking().ToList();
        }

        // Add methods for CRUD operations here

        public Category? GetById(int id)
        {
            return dbContext.Categories.Find(id);
        }

        public int Remove(Category category)
        {
            dbContext.Categories.Remove(category);
            return dbContext.SaveChanges();
        }

        public int Update(Category category)
        {
            dbContext.Categories.Update(category);
            return dbContext.SaveChanges();

        }
    }
}
