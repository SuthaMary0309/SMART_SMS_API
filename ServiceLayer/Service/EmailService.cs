using Microsoft.Extensions.Configuration;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            var settings = _config.GetSection("EmailSettings");

            var message = new MailMessage
            {
                From = new MailAddress(settings["SenderEmail"], settings["SenderName"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            message.To.Add(to);

            var smtp = new SmtpClient(settings["Host"])
            {
                Port = int.Parse(settings["Port"]),
                Credentials = new NetworkCredential(settings["SenderEmail"], settings["AppPassword"]),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);

            return true;
        }
    }
}
