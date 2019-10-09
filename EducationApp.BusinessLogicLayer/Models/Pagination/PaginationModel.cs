using EducationApp.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;


namespace EducationApp.BusinessLogicLayer.Models.Pagination
{
    public class PaginationModel<T> : BaseModel
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public PaginationModel()
        {
            Items = new List<T>();
        }
    }
}
