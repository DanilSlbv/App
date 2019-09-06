using EducationApp.DataAccessLayer.Entities.Base;
using System.Collections.Generic;

namespace EducationApp.DataAccessLayer.Entities
{
    public class AuthorInPrintingEditons:BaseModel
    {
        public int AuthorId { get; set; }
        public int PrintingEditionId { get; set; }
        public virtual Author Author { get; set; }
        public virtual PrintingEdition PrintingEdition { get; set; }
    }
}
