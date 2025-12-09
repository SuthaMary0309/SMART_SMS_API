using Microsoft.Extensions.Configuration;
using ServiceLayer.ServiceInterFace;
using SMART_SMS_API.DTOs;
using System.Net;
using System.Net.Mail;
using System.IO;
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

        public async Task<bool> SendEmailAsync(EmailDTO dto)
        {
            var settings = _config.GetSection("EmailSettings");

            var mail = new MailMessage()
            {
                From = new MailAddress(settings["SenderEmail"], settings["SenderName"]),
                Subject = dto.Subject,
                Body = dto.Body,
                IsBodyHtml = true
            };

            mail.To.Add(dto.To);

            var smtp = new SmtpClient(settings["Host"])
            {
                Port = int.Parse(settings["Port"]),
                Credentials = new NetworkCredential(settings["SenderEmail"], settings["AppPassword"]),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
            return true;
        }

        public async Task<bool> SendEmailWithAttachmentAsync(EmailAttachmentDTO dto)
        {
            var settings = _config.GetSection("EmailSettings");

            var mail = new MailMessage()
            {
                From = new MailAddress(settings["SenderEmail"], settings["SenderName"]),
                Subject = dto.Subject,
                Body = dto.Body,
                IsBodyHtml = true
            };

            mail.To.Add(dto.To);

            if (dto.File != null)
            {
                using var ms = new MemoryStream();
                await dto.File.CopyToAsync(ms);
                var attachment = new Attachment(ms, dto.File.FileName);
                mail.Attachments.Add(attachment);
            }

            var smtp = new SmtpClient(settings["Host"])
            {
                Port = int.Parse(settings["Port"]),
                Credentials = new NetworkCredential(settings["SenderEmail"], settings["AppPassword"]),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
            return true;
        }
    }
}



