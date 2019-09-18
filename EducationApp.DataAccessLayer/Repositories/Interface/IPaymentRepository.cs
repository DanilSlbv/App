using EducationApp.DataAccessLayer.Entities;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IPaymentRepository:IBaseEFRepository<Payment>
    {
        Task<Payment> GetByTransactionIdAsync(string transactionId);
    }
}
