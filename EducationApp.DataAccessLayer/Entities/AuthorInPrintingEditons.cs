using EducationApp.DataAccessLayer.Entities.Base;
using System;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class AuthorInPrintingEditons:BaseModel
    {
        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public string PrintingEditionId { get; set; }
        public  PrintingEdition PrintingEdition { get; set; }
        public DateTime Date { get; set; }
    }
}
