using EducationApp.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IPaymentRepository:IBaseEFRepository<Payment>
    {
        Task<List<Payment>> GetAllAsync();
        Payment GetByTransactionIdAsync(string transactionId);
        Task RemoveTransaction(int paymentId);
    }
}
