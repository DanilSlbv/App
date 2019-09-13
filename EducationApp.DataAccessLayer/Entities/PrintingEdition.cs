using EducationApp.DataAccessLayer.Entities.Base;
using EducationApp.DataAccessLayer.Entities.Enums;

namespace EducationApp.DataAccessLayer.Entities
{
    public class PrintingEdition:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public StatusEnumEntity status { get; set; }
        public CurrencyEnumEntity currency { get; set; }
        public TypeEnumEntity type { get; set; }

    }
}
