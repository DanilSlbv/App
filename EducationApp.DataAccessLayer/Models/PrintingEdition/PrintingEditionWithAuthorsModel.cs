
using System.Collections.Generic;
using EducationApp.DataAccessLayer.Entities;
using Type =  EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
using Currency = EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;

namespace EducationApp.DataAccessLayer.Models.PrintingEditions
{
    public class PrintingEditionWithAuthorsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<Author> Authors { get; set; }
        public Currency Currency { get; set; }
        public Type Type { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public PrintingEditionWithAuthorsModel()
        {

        }
    }
}
