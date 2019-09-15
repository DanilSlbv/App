using System;
using System.ComponentModel.DataAnnotations;


namespace EducationApp.DataAccessLayer.Entities.Base
{
    public class BaseModel
    {
        [Key]
        public string Id { get; set; }
    }
}
