//using EducationApp.BusinessLogicLayer.Models.Orders;
//using EducationApp.BusinessLogicLayer.Services.Interfaces;
//using EducationApp.DataAccessLayer.Repositories.Interface;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace EducationApp.BusinessLogicLayer.Services
//{
//    public class OrderService:IOrderService
//    {
//        private readonly IPaymentRepository _paymentRepository;
//        private readonly IOrderItemRepository _orderItemRepository;
//        private readonly IOrderRepository _orderRepository;
//        public OrderService(IPaymentRepository paymentRepository,IOrderItemRepository orderItemRepository,IOrderRepository orderRepository)
//        {
//            _paymentRepository = paymentRepository;
//            _orderItemRepository = orderItemRepository;
//            _orderRepository = orderRepository;
//        }

//        public async Task<string> AddToOrderAsync()
//        {

//        }

//        public async Task<bool> BuyAsync(OrderItemModel orderItemModel)
//        {
//            ICartService cartService;
//            cartService.Charge(orderItemModel);
//        }
//        public string Charge(string stripeEmail, string stripeToken, OrderItemModel orderItemModel)
//        {
//            var customers = new CustomerService();
//            var charges = new ChargeService();
//            var customer = customers.Create(new CustomerCreateOptions
//            {
//                Email = stripeEmail,
//                Source = stripeToken
//            });
//            var charge = charges.Create(new ChargeCreateOptions
//            {
//                Amount = (long)orderItemModel.Amount * 100,
//                Description = "FirstCharge",
//                Currency = orderItemModel.Currency.ToString(),
//                CustomerId = customer.Id
//            });
//            return charge.Status;
//        }

//    }
//}
