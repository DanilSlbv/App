using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Order;
using EducationApp.DataAccessLayer.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AscendingDescending= EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    public interface IOrderRepository:IBaseEFRepository<Order>
    {
        Task<List<Order>> GetAllAsync();
        Task AddOrder(Order order);
        Task RemoveAsync(int orderId);
        Task<PaginationModel<OrdersForAdminModel>> GetOrdersForAdmin(int page, AscendingDescending sortByOrderId, AscendingDescending sortByDate, AscendingDescending sortByOrderAmount);
        Task<PaginationModel<OrdersForUserModel>> GetUserOrders(int page, string userId);
    }
}
