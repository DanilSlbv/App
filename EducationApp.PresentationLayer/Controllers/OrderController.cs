//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Stripe;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace EducationApp.PresentationLayer.Controllers
//{
//    [AllowAnonymous]
//    [Controller]
//    [Route("order")]
//    public class OrderController:Controller
//    {
//        [HttpPost]
//        [Route("addtocart")]
//        public async Task<IActionResult> AddToCart(OrderItem orderItem)
//        {
//            ///localStorage.Add(orderItem)
//        }

//        public async Task<IActionResult> CheckOut()
//        {
//            //GetAllFromLocalStprage(orderItems)
//            //GetFromLocalStorage(TotalAmount)

//        }

//        public async Task<IActionResult> Buy()
//        {
//            //Charge(string stripeEmail,string stripeToken)
//            //if(succes) add To orders;
//        }









//        //[HttpGet]
//        //[Route("start")]
//        //public IActionResult Start()
//        //{
//        //    return View();
//        //}
//        //[Route("charge")]
//        //[HttpPost]
//        //public IActionResult Charge(string stripeEmail,string stripeToken)
//        //{
//        //    var customers = new CustomerService();
//        //    var charges = new ChargeService();
//        //    var customer = customers.Create(new CustomerCreateOptions
//        //    {
//        //        Email = stripeEmail,
//        //        Source = stripeToken
//        //    });
//        //    var charge = charges.Create(new ChargeCreateOptions
//        //    {
//        //        Amount = 500,
//        //        Description = "FirstCharge",
//        //        Currency = "usd",
//        //        CustomerId = customer.Id
//        //    });
//        //    if (charge.Status == "Succeeded")
//        //    {
//        //        string BalanceTransactionId = charge.BalanceTransactionId;
//        //        return View();
//        //    }
//        //    else
//        //    {

//        //    }
//        //    return View();
//        //}

//    }
//}
