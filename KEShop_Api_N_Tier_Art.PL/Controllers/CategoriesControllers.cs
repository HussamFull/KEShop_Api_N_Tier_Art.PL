using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;


namespace KEShop_Api_N_Tier_Art.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesControllers : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CategoriesControllers(ICategoryService categoryService, IStringLocalizer<SharedResource> localizer)
        {
            this.categoryService = categoryService;
            _localizer = localizer;
        }


        [HttpGet("")]
        public IActionResult GetAll([FromQuery] string lang = "en")
        {
            // استخدم Entities بدلاً من Categories
            var categories = categoryService.Entities
                .Include(c => c.CategoryTranslations) // لاحظ تصحيح الاسم الإملائي للخاصية 
                .Where(c => c.Status == Status.Active) // يمكنك إضافة شروط أخرى
                .ToList();

            var result = categories.Select(cat => new
            {
                Id = cat.Id,
                Name = cat.CategoryTranslations.FirstOrDefault(t => t.Language == lang)?.Name,
            });

            return Ok(new { message = _localizer["success"].Value, cats = result });
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
            if (result <= 0) return BadRequest( _localizer["Failed to create category"]);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { message = _localizer["Add-success-category"].Value,request } );
        }

        [HttpPatch("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] CategoryRequest request)
        {
            var updated = categoryService.Update(id, request);
           
            return updated >0 ? Ok() : NotFound(_localizer["Category not found or update failed"].Value);
        }

        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleStatus([FromRoute] int id)
        {
            var updated = categoryService.ToggleStatus(id);

            return updated ?   Ok(new { message= _localizer["Status toggled"] }) : NotFound(new { message = _localizer["Category toggled not found or update failed"].Value });
        }




        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = categoryService.Delete(id);
            if (deleted <= 0) return NotFound(_localizer["Category not found or delete failed"].Value);
            return Ok(new { message = _localizer["Category deleted successfully"].Value });

        }
    }
}
