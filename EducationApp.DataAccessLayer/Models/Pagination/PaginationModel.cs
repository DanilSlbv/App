using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.DataAccessLayer.Models.Pagination
{
    public class PaginationModel<T>
    {
        public List<T> Items { get; set; }
        public int ItemsCount { get; set; }
    }
}
