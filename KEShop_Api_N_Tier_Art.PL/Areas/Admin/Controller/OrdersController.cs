using KEShop_Api_N_Tier_Art.BLL.Services.Interfaces;
using KEShop_Api_N_Tier_Art.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KEShop_Api_N_Tier_Art.PL.Areas.Admin.Controller
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
   // [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Get All Orders by status
        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrderByStatus(OrderStatusEnum status)
        {
            var orders = await _orderService.GetByStatusAsync(status);
            return Ok(orders);
        }
        // Change Order Status
        [HttpPatch("change-status/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, [FromBody] OrderStatusEnum newStatus)
        {
            var result = await _orderService.ChangeStatusAsync(orderId, newStatus);
            if (!result)
            {
                return NotFound(new { Message = "Order not found or status unchanged." });
            }
            return Ok(new { Message = "Order status updated successfully." });
        }
    }
}
