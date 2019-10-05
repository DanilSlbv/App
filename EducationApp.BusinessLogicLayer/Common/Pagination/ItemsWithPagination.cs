using EducationApp.BusinessLogicLayer.Models.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EducationApp.BusinessLogicLayer.Common.Pagination
{
    public class ItemsWithPagination<T>
    {
        public PaginationItemModel<T> GetItems(int page,List<T> sourceItems)
        {
            int pageSize = 6;
            var count = sourceItems.Count();
            var items = sourceItems.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            var itemsAfterPaginationModel = new PaginationItemModel<T>()
            {
                Items = items,
                TotalPages=totalPages
            };
            return itemsAfterPaginationModel;
        }
    }
}
