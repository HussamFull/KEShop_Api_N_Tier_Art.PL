using KEShop_Api_N_Tier_Art.BLL.Services.Classes;
using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEShop_Api_N_Tier_Art.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
   // [Authorize(Roles = "Customer")]
    

    public class BrandsControllers : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandsControllers(IBrandService _brandService)
        {
            brandService = _brandService;
        }
        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var brands = brandService.GetAll(true);

        //    return Ok(brands);
        //}
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var brands = brandService.GetAll(false);

            // تعديل النتيجة لتحتوي رابط الصورة
            var result = brands.Select(b => new
            {
                b.Id,
                b.Name,
                MainImageUrl = string.IsNullOrEmpty(b.MainImage)
                    ? null
                   : Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", b.MainImage)
            });

            return Ok(result);
        }

        // ById 
        //[HttpGet("{id}")]
        //public IActionResult GetById([FromRoute] int id)
        //{
        //    var brand = brandService.GetById(id);
        //    if (brand is null) return NotFound();
        //    return Ok(brand);
        //}
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var brand = brandService.GetById(id);
            if (brand is null)
                return NotFound();

            var result = new
            {
                brand.Id,
                brand.Name,
                MainImagePath = string.IsNullOrEmpty(brand.MainImage)
                    ? null
                    : Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", brand.MainImage)
            };

            return Ok(result);
        }

    }
}
