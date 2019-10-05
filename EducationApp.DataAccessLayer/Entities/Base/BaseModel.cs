using System;
using System.ComponentModel.DataAnnotations;


namespace EducationApp.DataAccessLayer.Entities.Base
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public bool IsRemoved{get;set;}
        public DateTime CreationDate { get; set; }
        public BaseModel()
        {
            CreationDate = DateTime.Now;
            IsRemoved = false;
        }
    }
}
