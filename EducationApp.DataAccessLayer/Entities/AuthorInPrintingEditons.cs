using EducationApp.DataAccessLayer.Entities.Base;
using System;

namespace EducationApp.DataAccessLayer.Entities
{
    public class AuthorInPrintingEditons: BaseEntity
    {
        public long AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public long PrintingEditionId { get; set; }
        public virtual  PrintingEdition PrintingEdition { get; set; }
        public DateTime Date { get; set; }
    }
}
