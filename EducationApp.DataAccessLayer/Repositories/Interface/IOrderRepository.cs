using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Models.Filters;
using EducationApp.DataAccessLayer.Models.Order;
using EducationApp.DataAccessLayer.Models.Response;
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
        Task<ResponseModel<OrdersWithOrderItemsModel>> GetUserOrders(int page, string userId, OrderFilterModel filterModel);
    }
}
