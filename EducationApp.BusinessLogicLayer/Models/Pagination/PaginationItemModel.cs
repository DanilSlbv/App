using System.Collections.Generic;


namespace EducationApp.BusinessLogicLayer.Models.Pagination
{
    public class PaginationItemModel<T>
    {
        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
    }
}
