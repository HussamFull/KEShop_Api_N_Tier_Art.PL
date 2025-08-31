using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.DTO.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Security.Claims;


namespace KEShop_Api_N_Tier_Art.PL.Areas.Customer.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]

    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService )
        {
            _cartService = cartService;
        }

        [HttpPost("")]
        public IActionResult AddToCart([FromBody] CartRequest request)
        {
            // Get the UserId from the authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            var result = _cartService.AddToCart(request, userId);
            if (result)
            {
                return Ok("Product added to cart successfully.");
            }
            else
            {
                return BadRequest("Failed to add product to cart.");
            }
        }

        [HttpGet("")]

        public IActionResult GetUserCart()
        {
            // Get the UserId from the authenticated user
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            var result = _cartService.GetCartSummaryResponse(userId);
            return Ok(result);
        }
    }
}
