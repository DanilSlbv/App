using AscendingDescending = EducationApp.DataAccessLayer.Entities.Enums.Enums.AscendingDescending;

namespace EducationApp.DataAccessLayer.Models.Filters
{
    public class OrderFilterModel
    {
        public AscendingDescending SortByOrderId { get; set; }
        public AscendingDescending SortByDate { get; set; }
        public AscendingDescending SortByOrderAmount { get; set; }
    }
}
