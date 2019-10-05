using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Entities.Enums;
using EducationApp.DataAccessLayer.Repositories.Interface;
using Stripe;
using System;
using System.Linq;
using System.Threading.Tasks;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Enums.Type;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPrintingEditionRepository _printingEditionRepository;

        public OrderService(IPaymentRepository paymentRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IUserRepository userRepository, IPrintingEditionRepository printing)
        {
            _paymentRepository = paymentRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _printingEditionRepository = printing;
        }

        public async Task<UserOrdersInfoModel> GetUserOrdersAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            UserOrdersInfoModel userOrdersInfoModel = new UserOrdersInfoModel();
            foreach(var order in orders)
            {
                double AmountCount = 0;
                var orderItems = await _orderItemRepository.GetByOrderId(order.Id);
                var userOrdersInfo = new UserOrdersInfoModelItem();
                userOrdersInfo.OrderId = order.Id;
                userOrdersInfo.OrderDate = order.Date;
                foreach(var orderItem in orderItems)
                {
                    var printingEdition = await _printingEditionRepository.GetByIdAsync(orderItem.PrintingEditionId);
                    userOrdersInfo.Type.Add((Type)printingEdition.Type);
                    userOrdersInfo.Title.Add(printingEdition.Name);
                    AmountCount += orderItem.Amount;
                }
                userOrdersInfo.OrderAmount = AmountCount;
                userOrdersInfoModel.Items.Add(userOrdersInfo);
            }
            return userOrdersInfoModel;
        }

        public async Task<FullOrdersInfoModel> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            FullOrdersInfoModel fullOrdersInfoModel= new FullOrdersInfoModel();
            foreach (var order in orders)
            {
                double AmountCount = 0;
                var orderItems = await _orderItemRepository.GetByOrderId(order.Id);
                var applicationUser = await _userRepository.GetUserByIdAsync(order.UserId);
                var fullOrdersInfoModelItem= new FullOrdersInfoModelItem();
                fullOrdersInfoModelItem.OrderId = order.Id;
                fullOrdersInfoModelItem.OrderDate = order.Date;
                fullOrdersInfoModelItem.UserEmail = applicationUser.Email;
                fullOrdersInfoModelItem.UserName = applicationUser.FirstName+applicationUser.LastName;
                foreach (var orderItem in orderItems)
                {
                    var printingEdition = await _printingEditionRepository.GetByIdAsync(orderItem.PrintingEditionId);
                    fullOrdersInfoModelItem.Type.Add((Type)printingEdition.Type);
                    fullOrdersInfoModelItem.Title.Add(printingEdition.Name);
                    AmountCount += orderItem.Amount;
                }
                fullOrdersInfoModelItem.OrderAmount = AmountCount;
                fullOrdersInfoModel.Items.Add(fullOrdersInfoModelItem);
            }
            return fullOrdersInfoModel;
        }

        

        public async Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderIdAscendingAsync()
        {
            var fullOrdersInfoModel = await GetAllOrdersAsync();
            var sortOrders = fullOrdersInfoModel.Items.OrderBy(x => x.OrderId);
            return sortOrders;
        }
        public async Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderIdDescendingAsync()
        {
            var fullOrdersInfoModel = await GetAllOrdersAsync();
            var sortOrders = fullOrdersInfoModel.Items.OrderByDescending(x => x.OrderId);
            return sortOrders;
        }

        public async Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderDateAscendingAsync()
        {
            var fullOrdersInfoModel = await GetAllOrdersAsync();
            var sortOrders = fullOrdersInfoModel.Items.OrderBy(x => x.OrderDate);
            return sortOrders;
        }

        public async Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderDateDescendingAsync()
        {
            var fullOrdersInfoModel = await GetAllOrdersAsync();
            var sortOrders = fullOrdersInfoModel.Items.OrderByDescending(x => x.OrderDate);
            return sortOrders;
        }

        public async Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderAmountAscendingAsync()
        {
            var fullOrdersInfoModel = await GetAllOrdersAsync();
            var sortOrders = fullOrdersInfoModel.Items.OrderBy(x => x.OrderAmount);
            return sortOrders;
        }
        public async Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderAmountDescendingAsync()
        {
            var fullOrdersInfoModel = await GetAllOrdersAsync();
            var sortOrders = fullOrdersInfoModel.Items.OrderByDescending(x => x.OrderAmount);
            return sortOrders;
        }

        public async Task<bool> RemoveTransaction(int paymentId)
        {
            if (_paymentRepository.GetByIdAsync(paymentId) == null)
            {
                return false;
            }
            await _paymentRepository.RemoveTransaction(paymentId);
            return true;
        }
        
        public async Task AddTrasaction(string transactionId)
        {
            var payment = new Payment()
            {
                TransactionId = transactionId
            };
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
