using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.DTO.Responses;
using KEShop_Api_N_Tier_Art.DAL.Models;
using KEShop_Api_N_Tier_Art.DAL.Repositories.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEShop_Api_N_Tier_Art.BLL.Services.Classes
{
    public class ProductService : GenericService<ProductRequest, ProductResponse, Product>, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository repository, IFileService fileService) : base(repository)
        {
            _repository = repository;
            _fileService = fileService;
        }
   
        public async Task<int> CreateProduct(ProductRequest request)
        {
            var entity =  request.Adapt<Product>();
            entity.CreatedAt = DateTime.Now;

            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            if (request.SubImages != null)
            {
                var SubImagesPaths = await _fileService.UploadManyAsync(request.SubImages);
                entity.SubImages = SubImagesPaths.Select(img => new ProductImage { ImageName = img}).ToList();
            }
            return  _repository.Add(entity);
        }

        public async Task<List<ProductResponse>> GetAllProducts(HttpRequest request, bool onlayActive = false)
        {
            var products =  _repository.GetAllProductsWithImage();

            if (onlayActive)
            {
                products = products.Where(p => p.Status == Status.Active).ToList();
            }

            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Quantity = p.Quantity,
                MainImageUrl = $"{request.Scheme}://{request.Host}/Images/{p.MainImage}",
                SubImagesUrl = p.SubImages.Select(img => $"{request.Scheme}://{request.Host}/Images/{img.ImageName}").ToList()

            }).ToList();
        }
    }
}
