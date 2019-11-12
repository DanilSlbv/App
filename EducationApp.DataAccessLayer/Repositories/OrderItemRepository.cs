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
    public class OrderItemRepository: BaseEFRepository<OrderItem>, IOrderItemRepository
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";
        private readonly ApplicationContext _applicationContext;
        public OrderItemRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<OrderItem>> GetAllAsync() => await _applicationContext.OrderItems.Where(x => x.IsRemoved == false).ToListAsync();

        public List<OrderItem> GetAllAsyncDapper()
        {
            string sql = "SELECT * FROM [EducationStoreDb].[dbo].[OrderItems] WHERE IsRemoved=1";
            using (var db = new SqlConnection(_connectionString))
            {
                return db.Query<OrderItem>(sql).ToList();
            }
        }

        public async Task<List<OrderItem>> GetByOrderId(int orderId)
        {
            return await _applicationContext.OrderItems.Where(x => x.OrderId == orderId && x.IsRemoved == false).ToListAsync();
        }

        public List<OrderItem> GetByOtderIdDapper(long orderId)
        {
            string sql = $@"SELECT * FROM [EducationStoreDb].[dbo].[OrderItems] WHERE Id={orderId}";
            using (var db = new SqlConnection(_connectionString))
            {
                return db.Query<OrderItem>(sql).ToList();
            }
        }
    }
}
