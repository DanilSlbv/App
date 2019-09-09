using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EducationApp.BusinessLogicLayer.Helpers
{
    
    public class EmailHelpers
    {

        private SmtpClient _smtpClient;
        private string _userEmail;
        private string _code;
        private string _from;
        public EmailHelpers(string userEmail,string code)
        {
            _smtpClient =new SmtpClient();
            _userEmail = userEmail;
            _code = code;
            _from = "BookStore";
        }

        public async Task SendEmail()
        {
            string subject = "BookStore - Confirm the Email";
            string body = "To Confirm the email please follow the link:" + _code;
            await _smtpClient.SendMailAsync(new MailMessage(
                from: _from,
                to: _userEmail,
                subject: subject,
                body: body
                ));
           _smtpClient.Dispose();
        }
    }
}

