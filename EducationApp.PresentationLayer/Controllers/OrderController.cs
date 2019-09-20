using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.PresentationLayer.Controllers
{
    [AllowAnonymous]
    [Controller]
    [Route("order")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("Buy")]
        public IActionResult Buy()
        {
            return View();
        }

        [HttpPost("Charge")]
        public async Task<IActionResult> Charge(string stripeEmail, string stripeToken,long amount)
        {
            await _orderService.ChargeAsync(stripeEmail, stripeToken,amount);
            return View();
        }

    }
}
