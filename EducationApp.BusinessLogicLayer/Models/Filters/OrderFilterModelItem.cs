using AscendingDescending = EducationApp.BusinessLogicLayer.Models.Enums.Enums.AscendingDescending;

namespace EducationApp.BusinessLogicLayer.Models.Filters
{
    public class OrderFilterModelItem
    {
        public AscendingDescending SortByOrderId { get; set; }
        public AscendingDescending SortByDate { get; set; }
        public AscendingDescending SortByOrderAmount { get; set; }
    }
}
