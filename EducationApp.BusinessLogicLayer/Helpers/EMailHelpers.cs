using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Common;
namespace EducationApp.BusinessLogicLayer.Helpers
{

    public class EmailHelpers
    {
        private string _userEmail;
        private string _link;
        public EmailHelpers(string userEmail,string link)
        {
            _userEmail = userEmail;
            _link = link;
        }
        public async Task<bool> SendEmailAsync()
        {
           
            var credentials = new NetworkCredential("storebooksender@gmail.com", "Qwerty987456");
            var mail = new MailMessage()
            {
                From = new MailAddress("BookStore@book.com"),
                Subject = Constants.EmailHelper.EmailSubject,
                Body = Constants.EmailHelper.EmailBody + _link
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(_userEmail));
            var Clien = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = credentials
            };
            await Clien.SendMailAsync(mail);
            return true;
        }
    }
}

