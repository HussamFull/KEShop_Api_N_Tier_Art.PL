using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEShop_Api_N_Tier_Art.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesControllers : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoriesControllers(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var categories = categoryService.GetAll();
           
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var category = categoryService.GetById(id);
            if (category is null) return NotFound();
            return Ok(category);
        }
        [HttpPost]
        public IActionResult Create([FromBody] CategoryRequest request)
        {
          
            var result = categoryService.Create(request);
            if (result <= 0) return BadRequest("Failed to create category");
            return CreatedAtAction(nameof(GetById), new { id = result }, new { message = request } );
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated = categoryService.Update(id, request);
           
            return updated >0 ? Ok() : NotFound("Category not found or update failed");
        }

        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = categoryService.ToggleStatus(id);

            return updated ?   Ok(new { message= " Status toggled" }) : NotFound(new { message = "Category toggled not found or update failed" });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = categoryService.Delete(id);
            if (deleted <= 0) return NotFound("Category not found or delete failed");
            return Ok(new { message = "Category deleted successfully" });

        }
    }
}
