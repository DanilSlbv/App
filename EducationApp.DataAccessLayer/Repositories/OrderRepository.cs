using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Order;
using EducationApp.DataAccessLayer.Models.Response;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Models.Filters;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using Type = EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using EducationApp.DataAccessLayer.Common;
using Microsoft.AspNetCore.Identity;
using EducationApp.DataAccessLayer.Common.Constants;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class OrderRepository : BaseEFRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderRepository(ApplicationContext applicationContext, UserManager<ApplicationUser> userManager) : base(applicationContext)
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }

        public async Task<List<Order>> GetAllAsync() => await _applicationContext.Orders.Where(x => x.IsRemoved == false).ToListAsync();


        public async Task<ResponseModel<OrdersWithOrderItemsModel>> GetUserOrders(int page, string userId, OrderFilterModel filterModel)
        {
            var orders = _applicationContext.OrderItems.Include(x => x.Order).Include(x => x.PrintingEdition).AsQueryable();
            if (!string.IsNullOrWhiteSpace(userId))
            {
                orders = orders.Where(x => (x.IsRemoved == false) && x.Order.UserId == userId);
            }
            if (string.IsNullOrWhiteSpace(userId))
            {
                orders = orders.Where(x => x.IsRemoved == false);
            }
            if (filterModel.SortByOrderId == AscendingDescending.Ascending)
            {
                orders = orders.OrderBy(x => x.OrderId);
            }
            if (filterModel.SortByOrderId == AscendingDescending.Descending)
            {
                orders = orders.OrderByDescending(x => x.OrderId);
            }
            if (filterModel.SortByDate == AscendingDescending.Ascending)
            {
                orders = orders.OrderBy(x => x.Order.Date);
            }
            if (filterModel.SortByDate == AscendingDescending.Descending)
            {
                orders = orders.OrderByDescending(x => x.Order.Date);
            }
            if (filterModel.SortByOrderAmount == AscendingDescending.Ascending)
            {
                orders = orders.OrderBy(x => x.Amount);
            }
            if (filterModel.SortByOrderAmount == AscendingDescending.Descending)
            {
                orders = orders.OrderByDescending(x => x.Amount);
            }
            var userOrders = await orders.GroupBy(x => x.OrderId).Select(orderItem => new OrdersWithOrderItemsModel
            {
                OrderId = orderItem.Key,
                UserEmail = orderItem.Select(x => x.Order.User.UserName).FirstOrDefault(),
                OrderDate = orderItem.Select(x => x.Order.Date).FirstOrDefault(),
                PrintingType = orderItem.Select(x => x.PrintingEdition.Type).ToList(),
                PrintingEditions = orderItem.Select(x=>x.PrintingEdition).ToList(),
                OrderAmount = orderItem.Select(x => x.Amount).FirstOrDefault()
            }).Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            var result = new ResponseModel<OrdersWithOrderItemsModel>
            {
                Items = userOrders,
                ItemsCount = _applicationContext.Orders.Count()
            };
            return result;
        }
    }
}
