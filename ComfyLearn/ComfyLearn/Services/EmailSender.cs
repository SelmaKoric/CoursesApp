using ComfyLearn.Helper;
using ComfyLearn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComfyLearn.Services
{
    public class EmailSender : IEmailSender
    {
        public EmailOptions _options { get; set; }
        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
            _options = emailOptions.Value;
        }
        public Task SendEmailAsync(string email, string subject, string Message)
        {
            return Execute(_options.SendGridKey, subject, Message, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGridKey);
            var msg = new SendGridMessage()
            {
                //From = new EmailAddress("admin@gmail.com", "ComfyLearn"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            try
            {
                return client.SendEmailAsync(msg);
            }
            catch (Exception ex)
            {
               
            }

            return null;
        }
    }
}
