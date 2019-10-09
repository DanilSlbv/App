using EducationApp.BusinessLogicLayer.Models.Base;
using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.BusinessLogicLayer.Models.User
{
    public class UserModelItem: BaseModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserModelItem() { }
        public UserModelItem(ApplicationUser User)
        {
            Id = User.Id;
            FirstName = User.FirstName;
            LastName = User.LastName;
            Email = User.Email;
        }
    }
    
}
