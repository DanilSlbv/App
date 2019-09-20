using EducationApp.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class AuthorInPrintingEditons:BaseModel
    {
        public string AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public string PrintingEditionId { get; set; }
        public virtual  PrintingEdition PrintingEdition { get; set; }
        public DateTime Date { get; set; }
    }
}
