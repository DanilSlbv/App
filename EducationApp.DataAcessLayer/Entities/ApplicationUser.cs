
using Microsoft.AspNetCore.Identity;
namespace EducationApp.DataAcessLayer.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
    }
}
