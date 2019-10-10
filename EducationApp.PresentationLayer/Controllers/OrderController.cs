using EducationApp.BusinessLogicLayer.Models.Filters;
using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.PresentationLayer.Common.Constants;
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

        [HttpPost("getallorders/{page}/{userId}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> GetAllOrders(int page,string userId,OrderFilterModelItem filterModel)
        {
            var orders = await _orderService.GetAllOrdersAsync(page,userId,filterModel);
            return Ok(orders);
        }
        
        [HttpGet("removeorder/{orderId}")]
        [Authorize(Roles = Constants.Roles.AdmimRole)]
        public async Task<IActionResult> RemoveOrder(int orderId)
        {
            var result = await _orderService.RemoveOrderAsync(orderId);
            return Ok(result);
        }

        [HttpPost("charge")]
        public async Task<IActionResult> Charge(ChargeModelItem chargeModelItem)
        {
            var result = await _orderService.ChargeAsync(chargeModelItem);
            return Ok(result);
        }
    }
}
