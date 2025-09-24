using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Interfaces
{
    public  interface IProductService : IGenaricService<ProductRequest, ProductResponse, Product>
    {
        Task<int> CreateProduct(ProductRequest request);
        Task<List<ProductResponse>> GetAllProducts(HttpRequest request, bool onlayActive = false);
    }
}
