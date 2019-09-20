using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class OrderRepository: BaseEFRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _applicationContext;
        public OrderRepository(ApplicationContext applicationContext):base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task AddOrder(Order order)
        {
            _applicationContext.Orders.Add(order);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetUserOrdersAsync(string UserId)
        {
            return await _applicationContext.Orders.Where(x => x.UserId == UserId).ToListAsync();
        }
        //public async Task<IQueryable> GetFullOrdersInfoAsync()
        //{
        //    var result = from orders in _applicationContext.Orders
        //                 join user in _applicationContext.Users on orders.UserId equals user.Id
        //                 join payments in _applicationContext.Payments on orders.PaymentId equals payments.Id
        //                 select new
        //                 {
        //                     Id = orders.Id,
        //                     UserEmail = user.Email,
        //                     Date = orders.Date,
        //                     TransactionId = payments.TransactionId
        //                 };
        //    return result;
        //}
    }
}
