using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Travel.Api.Models.Communication;

namespace Travel.Communication.Email
{
    public class EmailService: IEmailService
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool SendEmail(EmailModel model)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(model.Alias,model.FromEmail));
                message.To.Add(new MailboxAddress(model.ToName, model.ToEmail));
                message.Subject = model.Subject;
                message.Body = new TextPart() { Text = model.Message };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    // email
            
                    //SMTP server authentication if needed
                    client.Authenticate("ndu.systems@gmail.com", "Harder01!");
                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
