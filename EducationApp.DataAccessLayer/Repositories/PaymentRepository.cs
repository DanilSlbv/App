using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using System.Collections.Generic;
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
       
        public async Task<Payment> GetByTransactionIdAsync(string transactionId)
        {
            return await _applicationContext.Payments.FindAsync(transactionId);
        }
    }
}
