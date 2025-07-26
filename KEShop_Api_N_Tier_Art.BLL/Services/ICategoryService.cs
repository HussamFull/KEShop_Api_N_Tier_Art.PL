using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services
{
    public interface ICategoryService
    {
      
        int CreateCategory( CategoryRequest request);
        IEnumerable<CategoryResponses> GetAllCategories();
        CategoryResponses? GetCategoryById(int id);
        int UpdateCategory( int id, CategoryRequest request);
        int DeleteCategory(int id);

        bool ToggleStatus(int id);

    }
}
