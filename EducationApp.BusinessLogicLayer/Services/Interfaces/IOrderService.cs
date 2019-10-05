using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.Payments;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<UserOrdersInfoModel> GetUserOrdersAsync(string userId);
        Task<FullOrdersInfoModel> GetAllOrdersAsync();
        Task<bool> RemoveTransaction(int paymentId);
        Task AddOrderAsync(string description, string transactionId, string userId);
        Task<bool> ChargeAsync(string userEmail,string toke,long amount);
        Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderIdAscendingAsync();
        Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderIdDescendingAsync();
        Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderDateAscendingAsync();
        Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderDateDescendingAsync();
        Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderAmountAscendingAsync();
        Task<IOrderedEnumerable<FullOrdersInfoModelItem>> SortByOrderAmountDescendingAsync();
    }
}
