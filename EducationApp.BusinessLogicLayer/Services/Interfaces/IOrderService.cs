using EducationApp.BusinessLogicLayer.Models.Orders;
using EducationApp.BusinessLogicLayer.Models.Payments;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderModel> GetUserOrdersAsync(string userId);
        Task<PaymentModel> GetAllTransactions();
        Task RemoveTransaction(string transactionId);
        Task AddOrderAsync(string description, string paymentId, string userId);
        Task<bool> ChargeAsync(string userEmail,string toke,long amount);
    }
}
