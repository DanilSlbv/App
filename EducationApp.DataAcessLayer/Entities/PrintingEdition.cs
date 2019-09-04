using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.DataAcessLayer.Entities
{
    public class PrintingEdition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public bool IsRemoved { get; set; }
        public enum Statuc { }
        public enum Currency { }
        public enum Type {  }

    }
}
