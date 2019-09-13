using System;
using System.ComponentModel.DataAnnotations;


namespace EducationApp.DataAccessLayer.Entities.Base
{
    public class BaseModel
    {
        [Key]
        public string Id { get; set; }
        public DateTime CreationData { get; set; }
        public BaseModel()
        {
            CreationData = DateTime.UtcNow;
        }
    }
}
