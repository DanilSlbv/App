using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.Pagination;
using System.Threading.Tasks;
using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PaginationModel<UserOrdersModelItem>> GetAllUserOrdersAsync(int page,string userId);
        Task<PaginationModel<AdminOrdersModelItem>> GetAllOrdersForAdminAsync(int page, AscendingDescending orderId, AscendingDescending date, AscendingDescending orderAmount);
        Task<bool> RemoveTransactionAsync(int paymentId);
        Task AddOrderAsync(string description, string transactionId, string userId);
        Task<bool> ChargeAsync(ChargeModelItem chargeModelItem);
        Task<bool> RemoveOrderAsync(int orderId);
    }
}
