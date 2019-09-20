using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using Stripe;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public OrderService(IPaymentRepository paymentRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<OrderModel> GetUserOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetUserOrdersAsync(userId);
            OrderModel orderModel = new OrderModel();
            foreach(var order in orders)
            {
                orderModel.Items.Add(new OrderModelItem(order));
            }
            return orderModel;
        }

        public async Task<PaymentModel> GetAllTransactions()
        {
            var transactions = await _paymentRepository.GetAllAsync();
            PaymentModel paymentModel = new PaymentModel();
            foreach(var transaction in transactions)
            {
                paymentModel.Items.Add(new PaymentItemModel(transaction));
            }
            return paymentModel;
        }

        public async Task RemoveTransaction(string transactionId)
        {
            await _paymentRepository.RemoveTransaction(transactionId);
        }
        
        public async Task AddTrasaction(string transaction)
        {
            Payment payment = new Payment() { TransactionId = transaction };
            await _paymentRepository.AddAsync(payment);
        }

        public async Task AddOrderAsync(string description,string transactionId,string userId)
        {
            var payment =  _paymentRepository.GetByTransactionIdAsync(transactionId);
            var order = new DataAccessLayer.Entities.Order()
            {
                Date = DateTime.UtcNow,
                Description = description,
                PaymentId = payment.Id,
                UserId = userId
            };
            await _orderRepository.AddOrder(order);
        }

        public async Task<bool> ChargeAsync(string stripeEmail, string token,long amount)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = token
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = amount*100,
                Description = "BookStore",
                Currency = "usd",
                CustomerId = customer.Id
            }) ;

            if (!charge.Paid)
            {
                return true;
            }
            var user=await _userRepository.GetUserByEmailAsync(stripeEmail);
            await AddTrasaction(charge.Id); 
            await AddOrderAsync(charge.Description,charge.Id,user.Id);
            return true;
        }
    }
}
