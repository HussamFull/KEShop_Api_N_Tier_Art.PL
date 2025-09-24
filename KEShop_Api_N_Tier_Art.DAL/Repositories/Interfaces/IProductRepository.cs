using KEShop_Api_N_Tier_Art.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task DecreaseQuantityAsync(List<(int productId, int quantity)> items);

        List<Product> GetAllProductsWithImage();

    }
}
