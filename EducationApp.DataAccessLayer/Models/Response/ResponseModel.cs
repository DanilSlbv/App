using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.DataAccessLayer.Models.Response
{
    public class ResponseModel<T>
    {
        public List<T> Items { get; set; }
        public int ItemsCount { get; set; }
    }
}
