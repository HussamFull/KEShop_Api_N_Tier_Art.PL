using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository CategoryRepository;

        public CategoryService(ICategoryRepository categoryRepository ) 
        {
            CategoryRepository = categoryRepository;
        }
        public int CreateCategory(CategoryRequest request)
        {
            var category = request.Adapt<Category>();

            return CategoryRepository.Add(category);


        }

        public int DeleteCategory(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) return 0;
            return CategoryRepository.Remove(category);
        }

        public IEnumerable<CategoryResponses> GetAllCategories()
        {
            var categories = CategoryRepository.GetAll();
            //if (categories is null || !categories.Any()) return Enumerable.Empty<CategoryResponses>();
            return categories.Adapt<IEnumerable<CategoryResponses>>();

        }

        public CategoryResponses? GetCategoryById(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) return null;
            return category.Adapt<CategoryResponses>();

        }

        public int UpdateCategory(int id, CategoryRequest request)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) return 0;

            category.Name = request.Name;
            // Map the request to the existing category
           // request.Adapt(category);
            // Update the category in the repository
            return CategoryRepository.Update(category);

        }

        public bool ToggleStatus(int id)
        {
            var category = CategoryRepository.GetById(id);
            if (category is null) return false;

            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active ;
            
             CategoryRepository.Update(category);
            return true;

        }
    }
}
