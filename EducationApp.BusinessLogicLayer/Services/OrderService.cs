using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using Stripe;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Models.Response;
using EducationApp.BusinessLogicLayer.Models.Filters;
using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Common.Constants;

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

        public async Task<ResponseModel<OrdersWithOrderItemsModelItem>> GetAllOrdersAsync(int page, string userId, OrderFilterModelItem filterModel)
        {
            var resultItems = new ResponseModel<OrdersWithOrderItemsModelItem>();
            if (await _userRepository.GetUserByIdAsync(userId) == null)
            {
                return null;
            }
            var ordersForUser = await _orderRepository.GetUserOrders(page, userId, Mapper.MapToOrderItems.MapToOrderFilter(filterModel));
            foreach (var order in ordersForUser.Items)
            {
                resultItems.Items.Add(Mapper.MapToOrders.MapToOrdersWithOrderItemsModelItem(order));
            }
            resultItems.TotalItems = ordersForUser.ItemsCount;
            return resultItems;
        }

        public async Task<BaseModel> RemoveOrderAsync(int orderId)
        {
            var baseModel = new BaseModel();
            if (await _orderRepository.GetByIdAsync(orderId) != null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            if(await _orderRepository.RemoveAsync(orderId))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> RemoveTransactionAsync(int paymentId)
        {
            var baseModel = new BaseModel();
            if (_paymentRepository.GetByIdAsync(paymentId) == null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            if(await _paymentRepository.RemoveAsync(paymentId))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> CreateOrderItemAsync(OrderItemModelItem order)
        {
            var baseModel = new BaseModel();
            if(await _orderItemRepository.CreateAsync(Mapper.MapToOrderItems.MapToOrderItem(order)))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> CreateTrasactionAsync(string transactionId)
        {
            var baseModel = new BaseModel();
            if( await _paymentRepository.CreateAsync(Mapper.MapToPayments.MapToPayment(transactionId)))
            {
                return baseModel;
            }
            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
        }

        public async Task<BaseModel> CreateOrderAsync(string description, string transactionId, string userId)
        {
            var baseModel = new BaseModel();
            var payment = _paymentRepository.GetByTransactionIdAsync(transactionId);
            if (payment == null)
            {
                baseModel.Errors.Add(Constants.Errors.NotFount);
                return baseModel;
            }
            if(await _orderRepository.CreateAsync(Mapper.MapToOrders.MapToOrder(description,payment.Id,userId)))
            {
                return null;
            }

            baseModel.Errors.Add(Constants.Errors.ErrorToUpdate);
            return baseModel;
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
            if (!charge.Paid)
            {
                return false;
            }
            var user = await _userRepository.GetUserByEmailAsync(chargeModelItem.StripeEmail);
            await CreateTrasactionAsync(charge.Id);
            await CreateOrderAsync(charge.Description, charge.Id, user.Id);
            return true;
        }
    }
}
