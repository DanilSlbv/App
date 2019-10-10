using System;
using System.ComponentModel.DataAnnotations;


namespace EducationApp.DataAccessLayer.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public bool IsRemoved{get;set;}
        public DateTime CreationDate { get; set; }
        public BaseEntity()
        {
            CreationDate = DateTime.Now;
            IsRemoved = false;
        }
    }
}
