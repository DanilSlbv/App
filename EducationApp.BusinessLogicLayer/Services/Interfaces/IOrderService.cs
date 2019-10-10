using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.BusinessLogicLayer.Models.Filters;
using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.Response;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseModel<OrdersWithOrderItemsModelItem>> GetAllOrdersAsync(int page, string userId, OrderFilterModelItem filterModel);
        Task<BaseModel> RemoveTransactionAsync(int paymentId);
        Task<BaseModel> CreateOrderAsync(string description, string transactionId, string userId);
        Task<bool> ChargeAsync(ChargeModelItem chargeModelItem);
        Task<BaseModel> RemoveOrderAsync(int orderId);
    }
}
