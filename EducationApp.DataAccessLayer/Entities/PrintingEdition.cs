using EducationApp.DataAccessLayer.Entities.Base;
using Status=EducationApp.DataAccessLayer.Entities.Enums.Enums.Status;
using Currency=EducationApp.DataAccessLayer.Entities.Enums.Enums.Currency;
using Type=EducationApp.DataAccessLayer.Entities.Enums.Enums.Type;
namespace EducationApp.DataAccessLayer.Entities
{
    public class PrintingEdition:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Status Status { get; set; }
        public Currency Currency { get; set; }
        public Type Type { get; set; }

    }
}
