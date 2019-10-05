using EducationApp.BusinessLogicLayer.Common.Pagination;
using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllUserOrders(string id)
        {
            var orders = await _orderService.GetUserOrdersAsync(id);
            return Ok(orders);
        }

        [HttpGet("getallorders/{page}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllOrder(int page)
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var itemsWithPagination = new ItemsWithPagination<FullOrdersInfoModelItem>();
            var resultItems = itemsWithPagination.GetItems(page, orders.Items);
            return Ok(resultItems);
        }
        [HttpGet("sortbyidasc")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SortOrdersByIdAscending()
        {
            var orders = await _orderService.SortByOrderIdAscendingAsync();
            return Ok(orders);
        }

        [HttpGet("sortbyiddesc")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SortOrdersByIdDescending()
        {
            var orders = await _orderService.SortByOrderIdDescendingAsync();
            return Ok(orders);
        }

        [HttpGet("sortbydateasc")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SortOrdersByDateAscending()
        {
            var orders = await _orderService.SortByOrderIdAscendingAsync();
            return Ok(orders);
        }

        [HttpGet("sortbydatedesc")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SortOrdersByDateDescending()
        {
            var orders = await _orderService.SortByOrderDateAscendingAsync();
            return Ok(orders);
        }

        [HttpGet("sortbyamountasc")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SortOrdersByAmountAscending()
        {
            var orders = await _orderService.SortByOrderAmountAscendingAsync();
            return Ok(orders);
        }

        [HttpGet("sortbyamountdesc")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SortOrdersByAmountDescending()
        {
            var orders = await _orderService.SortByOrderAmountDescendingAsync();
            return Ok(orders);
        }

        [HttpGet("buy")]
        [Authorize(Roles = "admin,user")]
        public IActionResult Buy()
        {
            return View();
        }

        [HttpPost("charge")]
        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken,long amount)
        {
            await _orderService.ChargeAsync(stripeEmail, stripeToken,amount);
            return View();
        }

    }
}
