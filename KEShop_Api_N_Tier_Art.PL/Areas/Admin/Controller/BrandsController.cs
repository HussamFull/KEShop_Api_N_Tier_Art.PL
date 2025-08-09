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
    public class BrandsController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandsController(IBrandService _brandService)
        {
            brandService = _brandService;
        }
        [HttpGet("")]
        public IActionResult GetAll()
        {
            var brands = brandService.GetAll(false);

            return Ok(brands);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var brand = brandService.GetById(id);
            if (brand is null) return NotFound();
            return Ok(brand);
        }


        [HttpPost]
        public IActionResult Create([FromBody] BrandRequest request)
        {

            var result = brandService.Create(request);
            if (result <= 0) return BadRequest("Failed to create Brand");
            return CreatedAtAction(nameof(GetById), new { id = result }, new { message = request });
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] BrandRequest request)
        {
            var updated = brandService.Update(id, request);

            return updated > 0 ? Ok() : NotFound("Brand not found or update failed");
        }

        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = brandService.ToggleStatus(id);

            return updated ? Ok(new { message = " Status toggled" }) : NotFound(new { message = "Brand toggled not found or update failed" });
        }



        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = brandService.Delete(id);
            if (deleted <= 0) return NotFound("Brand not found or delete failed");
            return Ok(new { message = "Brand deleted successfully" });

        }
    }
}
