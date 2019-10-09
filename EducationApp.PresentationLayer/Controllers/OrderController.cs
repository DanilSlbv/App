using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.PresentationLayer.Controllers
{
    [Authorize]
    [Controller]
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("getalluserorders/{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetAllUserOrders(int page,string id)
        {
            var orders = await _orderService.GetAllUserOrdersAsync(page,id);
            return Ok(orders);
        }

        [HttpGet("getallorders/{page}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllOrder(int page, AscendingDescending orderId, AscendingDescending date, AscendingDescending orderAmount)
        {
            var orders = await _orderService.GetAllOrdersForAdminAsync(page, orderId, date, orderAmount);
            return Ok(orders);
        }

        public async Task<IActionResult> RemoveOrder(int orderId)
        {
            await _orderService.RemoveOrderAsync(orderId);
            return Ok(true);
        }

        [HttpPost("charge")]
        public async Task<IActionResult> Charge(ChargeModelItem chargeModelItem)
        {
            var result=await _orderService.ChargeAsync(chargeModelItem);
            return Ok(result);
        }
    }
}
