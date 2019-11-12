using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Orders;
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
using Microsoft.AspNetCore.Identity;
using EducationApp.DataAccessLayer.Common.Constants;
using System.Data.SqlClient;
using Dapper;
using System;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class OrderRepository : BaseEFRepository<Order>, IOrderRepository
    {
        private string _connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=EducationStoreDb;Trusted_Connection=True;MultipleActiveResultSets=True";
        private readonly ApplicationContext _applicationContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public OrderRepository(ApplicationContext applicationContext, UserManager<ApplicationUser> userManager) : base(applicationContext)
        {
            _applicationContext = applicationContext;
            _userManager = userManager;
        }

        public async Task<List<Order>> GetAllAsync() => await _applicationContext.Orders.Where(x => x.IsRemoved == false).ToListAsync();

        public async Task<List<Order>> GetAllAsyncDapper()
        {
            string sql = "SELECT * FROM [EducationStoreDb].[dbo].[Orders]";
            using (var db = new SqlConnection(_connectionString))
            {
                var result = db.Query<Order>(sql).ToList();
                return result;
            }
        }

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
                PrintingTypes = orderItem.Select(x => x.PrintingEdition.Type).ToList(),
                PrintingEditions = orderItem.Select(x => x.PrintingEdition).ToList(),
                OrderAmount = orderItem.Select(x => x.Amount).FirstOrDefault()
            }).Skip((page - 1) * Constants.Pagination.PageSize).Take(Constants.Pagination.PageSize).ToListAsync();
            var result = new ResponseModel<OrdersWithOrderItemsModel>
            {
                Items = userOrders,
                ItemsCount = _applicationContext.Orders.Where(x => x.IsRemoved == false).Count()
            };
            return result;
        }

        /*public async Task<OrdersWithOrderItemsModel> GetUserOrdersAsyncDapper()
        {
            string sql = @"SELECT O.*,OI.*,U.Id,PE.*
                          FROM [EducationStoreDb].[dbo].[OrderItems] AS OI
                          INNER JOIN [EducationStoreDb].[dbo].[Orders] AS O ON OI.OrderId=O.Id
                          INNER JOIN [EducationStoreDb].[dbo].[AspNetUsers] AS U ON O.UserId=U.Id
                          INNER JOIN [EducationStoreDb].[dbo].[PrintingEditions] AS PE ON OI.Id=PE.Id";
            var response = new ResponseModel<OrdersWithOrderItemsModel>();
            using (var db = new SqlConnection(_connectionString))
            {
                try
                {
                    var resultItems = new Dictionary<long, OrdersWithOrderItemsModel>();
                    await db.QueryAsync<Order, OrderItem, ApplicationUser, PrintingEdition, OrdersWithOrderItemsModel>(sql,
                        (order, orderItem, applicationUser, printingEdition) =>
                        {
                            var orderWithOrderItemsModel = new OrdersWithOrderItemsModel();
                            orderWithOrderItemsModel.OrderId = order.Id;
                            orderWithOrderItemsModel.OrderDate = order.Date;
                            orderWithOrderItemsModel.OrderAmount = orderItem.Amount;
                            resultItems.Add(orderWithOrderItemsModel.OrderId, orderWithOrderItemsModel);
                            return orderWithOrderItemsModel;
                        }                            
                         orderWithOrderItemsModel.PrintingEditions.Add(orderItem.PrintingEdition);
                         orderWithOrderItemsModel.PrintingTypes.Add(orderItem.PrintingEdition.Type);
                        );
                    return null;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }*/
    }
}
