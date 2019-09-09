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

        private SmtpClient _smtpClient;
        private string _userEmail;
        private string _code;
        public EmailHelpers(string userEmail,string code)
        {
            _smtpClient =new SmtpClient();
            _userEmail = userEmail;
            _code = code;
           
        }

        public async Task SendEmail()
        {

            await _smtpClient.SendMailAsync(new MailMessage(
                from: Constants.EmailHelper.EmailFromWho,
                to: _userEmail,
                subject: Constants.EmailHelper.EmailSubject,
                body: Constants.EmailHelper.EmailBody + _code
                ));
           _smtpClient.Dispose();
        }
    }
}

