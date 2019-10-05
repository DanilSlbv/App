using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<List<Payment>> GetAllAsync() => await _applicationContext.Payments.Where(x => x.IsRemoved == false).ToListAsync();

        public Payment GetByTransactionIdAsync(string transactionId)
        {
            return  _applicationContext.Payments.FirstOrDefault(x=>x.TransactionId==transactionId && x.IsRemoved == false);
        }
        public async Task RemoveTransaction(int paymentId)
        {
            var payment = await _applicationContext.Authors.FindAsync(paymentId);
            payment.IsRemoved = true;
            _applicationContext.Authors.Update(payment);
            await _applicationContext.SaveChangesAsync();
        }
    }
}
