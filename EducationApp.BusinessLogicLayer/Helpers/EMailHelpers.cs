using System.Net.Mail;
using System.Text;
using EducationApp.BusinessLogicLayer.Models.User;
namespace EducationApp.BusinessLogicLayer.Helpers
{
    public  class EMailHelpers
    {
        public static string UserEmail { get; set; }
        public EMailHelpers(string Email)
        {
            UserEmail = Email;
        }

        static MailAddress MailAddressSender = new MailAddress("BookStore@book.com", "BookStore");
        static MailAddress MailAddressReciever = new MailAddress(UserEmail);
        static MailMessage mailMessage = new MailMessage(MailAddressSender, MailAddressReciever);

    }
}
