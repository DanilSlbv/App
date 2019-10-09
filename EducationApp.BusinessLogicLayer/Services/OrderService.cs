using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using Stripe;
using System;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;
using CurrencyConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using AscDescConvert = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using EducationApp.BusinessLogicLayer.Models.Pagination;

namespace EducationApp.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        public OrderService(IPaymentRepository paymentRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _paymentRepository = paymentRepository;
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public async Task<PaginationModel<UserOrdersModelItem>> GetAllUserOrdersAsync(int page, string userId)
        {
            if (await _userRepository.GetUserByIdAsync(userId) == null)
            {
                return null;
            }
            var resultItems = new PaginationModel<UserOrdersModelItem>();
            var orders = await _orderRepository.GetUserOrders(page,userId);
            foreach (var order in orders.Items)
            {
                resultItems.Items.Add(new UserOrdersModelItem(order));
            }
            resultItems.TotalItems = orders.ItemsCount;
            return resultItems;
        }

        public async Task<PaginationModel<AdminOrdersModelItem>> GetAllOrdersForAdminAsync(int page, AscendingDescending sortByOrderId, AscendingDescending sortByDate, AscendingDescending sortByOrderAmount)
        {
            var resultItems = new PaginationModel<AdminOrdersModelItem>();
            var orders = await _orderRepository.GetOrdersForAdmin(page, (AscDescConvert)sortByOrderId, (AscDescConvert)sortByDate, (AscDescConvert)sortByOrderAmount);
            foreach (var order in orders.Items)
            {
                resultItems.Items.Add(new AdminOrdersModelItem(order));
            }
            resultItems.TotalItems = orders.ItemsCount;
            return resultItems;
        }

        public async Task<bool> RemoveOrderAsync(int orderId)
        {
            if (await _orderRepository.GetByIdAsync(orderId) != null)
            {
                await _orderRepository.RemoveAsync(orderId);
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveTransactionAsync(int paymentId)
        {
            if (_paymentRepository.GetByIdAsync(paymentId) == null)
            {
                return false;
            }
            await _paymentRepository.RemoveTransaction(paymentId);
            return true;
        }

        public async Task AddOrderItemAsync(OrderItemModelItem order)
        {
            var orderItem = new DataAccessLayer.Entities.OrderItem()
            {
                Amount = order.Amount,
                Currency = (CurrencyConvert)order.Currency,
                PrintingEditionId = order.PrintingEditionId,
                Count = order.Count,
                OrderId = order.OrderId
            };
            await _orderItemRepository.AddAsync(orderItem);
        }

        public async Task AddTrasactionAsync(string transactionId)
        {
            var payment = new Payment()
            {
                TransactionId = transactionId
            };
            await _paymentRepository.AddAsync(payment);
        }

        public async Task AddOrderAsync(string description, string transactionId, string userId)
        {
            var payment = _paymentRepository.GetByTransactionIdAsync(transactionId);
            var order = new DataAccessLayer.Entities.Order()
            {
                Date = DateTime.UtcNow,
                Description = description,
                PaymentId = payment.Id,
                UserId = userId
            };
            await _orderRepository.AddOrder(order);
        }

        public async Task<bool> ChargeAsync(ChargeModelItem chargeModelItem)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = chargeModelItem.StripeEmail,
                Source = chargeModelItem.StripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = chargeModelItem.Amount * 100,
                Description = "BookStore",
                Currency = "usd",
                CustomerId = customer.Id
            });

            if (charge.Paid)
            {
                return false;
            }
            var user = await _userRepository.GetUserByEmailAsync(chargeModelItem.StripeEmail);
            await AddTrasactionAsync(charge.Id);
            await AddOrderAsync(charge.Description, charge.Id, user.Id);
            return true;
        }
    }
}
