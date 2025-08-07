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
    [Authorize(Roles = "Customer")]
    

    public class BrandsControllers : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandsControllers(IBrandService _brandService)
        {
            brandService = _brandService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var brands = brandService.GetAll(true);

            return Ok(brands);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var brand = brandService.GetById(id);
            if (brand is null) return NotFound();
            return Ok(brand);
        }
       
    }
}
