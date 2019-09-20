using EducationApp.BusinessLogicLayer.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using Type = EducationApp.BusinessLogicLayer.Models.Enums.Type;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class EditPrintingEditionModelItem
    {
        public  string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public Status status { get; set; }
        public Currency currency { get; set; }
        public Type type { get; set; }
    }
}
