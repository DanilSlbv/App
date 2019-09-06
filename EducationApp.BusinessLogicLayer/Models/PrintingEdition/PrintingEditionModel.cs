using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    class PrintingEditionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public enum Status { }
        public enum Currency { }
        public enum Type { }
    }
}
