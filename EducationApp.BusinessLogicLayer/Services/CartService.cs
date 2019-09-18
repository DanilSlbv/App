//using EducationApp.BusinessLogicLayer.Models.Cart;
//using EducationApp.BusinessLogicLayer.Models.Orders;
//using EducationApp.BusinessLogicLayer.Services.Interfaces;
//using EducationApp.DataAccessLayer.Repositories.Interface;
//using Stripe;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EducationApp.BusinessLogicLayer.Services
//{
//    public class CartService : ICartService
//    {
//        private readonly IPaymentRepository _paymentRepository;
//        public CartService(IPaymentRepository paymentRepository)
//        {
//            _paymentRepository = paymentRepository;
//        }

//        public async Task<bool> AddToCart(OrderItemModel orderItemModel)
//        {
//            CartModel cartModel = new CartModel();
//        }
//        public async Task<CartModel> ListAll()
//        {

//        }
//        public async Task<double> GetAmount(CartModel cartModel)
//        {
//            double amount=cartModel.Items.Sum(x=>x.Amount)
//            return amount;
//        }
//    }
//}
