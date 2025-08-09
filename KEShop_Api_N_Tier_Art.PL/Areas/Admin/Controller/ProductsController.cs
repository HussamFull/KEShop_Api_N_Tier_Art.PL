using KEShop_Api_N_Tier_Art.BLL.Services.Classes;
using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEShop_Api_N_Tier_Art.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
       

        public ProductsController(IProductService ProductService)
        {
            _productService = ProductService;
        }
        [HttpGet("")]
        public IActionResult GetAll() => Ok(_productService.GetAll());
      

        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequest request)
        {
                var result = await _productService.CreateFile(request);
                return Ok(result);           
        }

    }
}
