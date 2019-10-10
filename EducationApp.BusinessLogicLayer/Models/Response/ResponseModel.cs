using EducationApp.BusinessLogicLayer.Models.Base;
using System.Collections.Generic;


namespace EducationApp.BusinessLogicLayer.Models.Response
{
    public class ResponseModel<T> : BaseModel
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public ResponseModel()
        {
            Items = new List<T>();
        }
    }
}
