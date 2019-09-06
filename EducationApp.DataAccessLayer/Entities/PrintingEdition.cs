using EducationApp.DataAccessLayer.Entities.Base;

namespace EducationApp.DataAccessLayer.Entities
{
    public class PrintingEdition:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsRemoved { get; set; }
        public enum Status { }
        public enum Currency { }
        public enum Type { }

    }
}
