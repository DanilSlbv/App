using EducationApp.BusinessLogicLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class AddPrintingEditionModelItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public Status status { get; set; }
        public Currency currency { get; set; }
        public DataAccessLayer.Entities.Enums.Type type { get; set; }
    }
}
