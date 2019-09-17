﻿using EducationApp.BusinessLogicLayer.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Models.PrintingEdition
{
    public class PrintingEditionModel:BaseModel
    {
        public List<PrintingEditionModelItem> Items { get; set; }
        public PrintingEditionModel()
        {
            Items = new List<PrintingEditionModelItem>();
        }
    }
}
