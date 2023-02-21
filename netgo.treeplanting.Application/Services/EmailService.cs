using Microsoft.AspNet.Identity;
using SendGrid.Helpers.Mail;
using System.Net;
using System.Configuration;
using SendGrid;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace netgo.treeplanting.Application.Services
{
    public class EmailService : IIdentityMessageService
    {
        private readonly IConfiguration _config; 
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridAsync(message);
        }


        private async Task ConfigSendGridAsync(IdentityMessage message)
        {            
            var apiKey = _config["Sendgrid:Key"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("netgo.treeplanting@gmail.com", "Treeplanter");
            var subject = message.Subject;
            var to = new EmailAddress(message.Destination);
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Send the email.
            if (client != null)
            {
                await client.SendEmailAsync(msg);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }
    }
}
