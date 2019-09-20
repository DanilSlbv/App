using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PaymentRepository : BaseEFRepository<Payment>, IPaymentRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PaymentRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public Payment GetByTransactionIdAsync(string transactionId)
        {
            return  _applicationContext.Payments.FirstOrDefault(x=>x.TransactionId==transactionId);
        }
        public async Task RemoveTransaction(string transactionId)
        {
            var transaction=await _applicationContext.Payments.FindAsync(transactionId);
            _applicationContext.Payments.Remove(transaction);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
