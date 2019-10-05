using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Common;
using Microsoft.Extensions.Configuration;

namespace EducationApp.BusinessLogicLayer.Helpers
{

    public class EmailHelpers
    {
        private readonly string _userEmail;
        private readonly IConfiguration _configuration;
        public EmailHelpers(string userEmail,IConfiguration configuration)
        {
            _userEmail = userEmail;
            _configuration = configuration;
        }

        public MailMessage MailMessageForPasswordRecovery(string newPassword)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(EducationApp.BusinessLogicLayer.Common.Extensions.Constants.EmailHelper.MailAddress),
                Subject = EducationApp.BusinessLogicLayer.Common.Extensions.Constants.EmailHelper.EmailSubject,
                Body = EducationApp.BusinessLogicLayer.Common.Extensions.Constants.EmailHelper.PasswordRecoveryEmailBody + newPassword
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(_userEmail));
            return mail;
        }
        public MailMessage MailMessageForEmailConfirm(string link) { 
            var mail = new MailMessage()
            {
                From = new MailAddress(EducationApp.BusinessLogicLayer.Common.Extensions.Constants.EmailHelper.MailAddress),
                Subject = EducationApp.BusinessLogicLayer.Common.Extensions.Constants.EmailHelper.EmailSubject,
                Body = EducationApp.BusinessLogicLayer.Common.Extensions.Constants.EmailHelper.ConfirmEmailLink + link
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(_userEmail));
            return mail;
        }

        public async Task<bool> SendEmailAsync(MailMessage mail)
        {
            var credentials = new NetworkCredential("storebooksender@gmail.com","Qwerty987456");
            try
            {
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
            catch (Exception ex)
            {
                var exp = ex;
                return false;

            }
        }
    }
}

