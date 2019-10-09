using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Order;
using EducationApp.DataAccessLayer.Models.Pagination;
using EducationApp.DataAccessLayer.Repositories.Base;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;
using EducationApp.DataAccessLayer.Common;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class OrderRepository : BaseEFRepository<Order>, IOrderRepository
    {
        private readonly ApplicationContext _applicationContext;
        public OrderRepository(ApplicationContext applicationContext) : base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<List<Order>> GetAllAsync() => await _applicationContext.Orders.Where(x => x.IsRemoved == false).ToListAsync();

        public async Task AddOrder(Order order)
        {
            _applicationContext.Orders.Add(order);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(int orderId)
        {
            var order = await _applicationContext.Authors.FindAsync(orderId);
            order.IsRemoved = true;
            _applicationContext.Authors.Update(order);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<PaginationModel<OrdersForUserModel>> GetUserOrders(int page,string userId)
        {
            var itemsCount = _applicationContext.Orders.Count();
            var items = await (from order in _applicationContext.Orders
                               where order.UserId==userId
                               select new OrdersForUserModel
                               {
                                   OrderId = order.Id,
                                   OrderDate = order.Date,
                                   PrintingType = (from orderItem in _applicationContext.OrderItems
                                                   join printingEdition in _applicationContext.PrintingEditions on orderItem.PrintingEditionId equals printingEdition.Id
                                                   select printingEdition.Type.ToString()
                                          ).ToList(),
                                   PrintingTitle = (from orderItem in _applicationContext.OrderItems
                                                  join printingEdition in _applicationContext.PrintingEditions on orderItem.PrintingEditionId equals printingEdition.Id
                                                  select printingEdition.Type.ToString()
                                          ).ToList(),
                                   OrderAmount = _applicationContext.OrderItems.Where(x => x.OrderId == order.Id).Select(x => x.Amount).Sum()
                               }).Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            var result = new PaginationModel<OrdersForUserModel>
            {
                Items = items,
                ItemsCount = itemsCount,
            };
            return result;
        }


        public async Task<PaginationModel<OrdersForAdminModel>> GetOrdersForAdmin(int page, AscendingDescending sortByOrderId, AscendingDescending sortByDate, AscendingDescending sortByOrderAmount)
        {
            var itemsCount = _applicationContext.Orders.Count();
            var items = await (from order in _applicationContext.Orders
                               select new OrdersForAdminModel
                               {
                                   OrderId = order.Id,
                                   OrderDate = order.Date,
                                   UserEmail = _applicationContext.Users.Where(x => x.Id == order.UserId).Select(x => x.Email).ToString(),
                                   PrintingType = (from orderItem in _applicationContext.OrderItems
                                               join printingEdition in _applicationContext.PrintingEditions on orderItem.PrintingEditionId equals printingEdition.Id
                                               select printingEdition.Type.ToString()
                                          ).ToList(),
                                   PrintingTitle = (from orderItem in _applicationContext.OrderItems
                                                  join printingEdition in _applicationContext.PrintingEditions on orderItem.PrintingEditionId equals printingEdition.Id
                                                  select printingEdition.Type.ToString()
                                          ).ToList(),
                                   OrderAmount = _applicationContext.OrderItems.Where(x => x.OrderId == order.Id).Select(x => x.Amount).Sum()
                               }).Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            var result = new PaginationModel<OrdersForAdminModel>
            {
                Items = items,
                ItemsCount = itemsCount,
            };
            return result;
        }
    }
}
