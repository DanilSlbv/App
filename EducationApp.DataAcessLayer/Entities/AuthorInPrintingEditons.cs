using System;


namespace EducationApp.DataAcessLayer.Entities
{
    public  class AuthorInPrintingEditons
    {
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PrintingEditionId { get; set; }
        public PrintingEdition PrintingEdition { get; set; }
        public DateTime Date { get; set; }
    }
}
