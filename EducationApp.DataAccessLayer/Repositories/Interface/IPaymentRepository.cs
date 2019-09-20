using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IPaymentRepository:IBaseEFRepository<Payment>
    {
        Payment GetByTransactionIdAsync(string transactionId);
        Task RemoveTransaction(string transactionId);
    }
}
