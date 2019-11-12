using Dapper;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class PaymentRepository : BaseEFRepository<Payment>, IPaymentRepository
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";
        private readonly ApplicationContext _applicationContext;

        public PaymentRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Payment>> GetAllAsync() => await _applicationContext.Payments.Where(x => x.IsRemoved == false).ToListAsync();

        public List<Payment> GetAllAsyncDapper()
        {
            var result = new List<Payment>();
            string sql = "SELECT * FROM [EducationStoreDb].[dbo].[Payments]";
            using(var db=new SqlConnection(_connectionString))
            {
                result = db.Query<Payment>(sql).ToList();
            }
            return result;
        }
             
        public Payment GetByTransactionIdAsync(string transactionId)
        {
            return  _applicationContext.Payments.FirstOrDefault(x=>x.TransactionId==transactionId && x.IsRemoved == false);
        }

        public Payment GetByTranstactionId(string transactionId)
        {
            string sql = $"SELECT * FROM [EducationStoreDb].[dbo].[Payments] WHERE TransactionId={transactionId}";
            using (var db = new SqlConnection(_connectionString))
            {
                return db.Query(sql).FirstOrDefault();
            }
        }
    }
}
