using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using EducationApp.BusinessLogicLayer.Common;
using EducationApp.BusinessLogicLayer.Common.Constants;
using EducationApp.BusinessLogicLayer.Models.Base;
using Microsoft.Extensions.Configuration;

namespace EducationApp.BusinessLogicLayer.Helpers
{

    public class EmailHelpers
    {
        private readonly string _userEmail;
        public EmailHelpers(string userEmail)
        {
            _userEmail = userEmail;
        }

        public MailMessage MailMessageForPasswordRecovery(string newPassword)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(Constants.EmailHelper.MailAddress),
                Subject = Constants.EmailHelper.EmailSubject,
                Body = Constants.EmailHelper.PasswordRecoveryEmailBody + newPassword
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(_userEmail));
            return mail;
        }

        public MailMessage MailMessageForEmailConfirm(string link)
        {
            var mail = new MailMessage()
            {
                From = new MailAddress(Constants.EmailHelper.MailAddress),
                Subject = Constants.EmailHelper.EmailSubject,
                Body = Constants.EmailHelper.ConfirmEmailLink + link
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(_userEmail));
            return mail;
        }

        public async Task<BaseModel> SendEmailAsync(MailMessage mail)
        {
            var baseModel = new BaseModel();
            var credentials = new NetworkCredential("storebooksender@gmail.com", "Qwerty987456");
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
                return baseModel;
            }
            catch (Exception ex)
            {
                baseModel.Errors.Add(ex.Message);
                return baseModel;

            }
        }
    }
}

