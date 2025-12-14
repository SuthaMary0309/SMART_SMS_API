using Microsoft.Extensions.Configuration;
using ServiceLayer.ServiceInterFace;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using ServiceLayer.DTO;
using ServiceLayer.ServiceInterFace;

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
            try
            {
                var smtp = new SmtpClient
                {
                    Host = _config["EmailSettings:Host"],
                    Port = int.Parse(_config["EmailSettings:Port"]),
                    EnableSsl = true, // ✅ REQUIRED
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false, // ❌ MUST BE FALSE
                    Credentials = new NetworkCredential(
                        _config["EmailSettings:SenderEmail"],
                        _config["EmailSettings:AppPassword"]
                    )
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(
                        _config["EmailSettings:SenderEmail"],
                        _config["EmailSettings:SenderName"]
                    ),
                    Subject = dto.Subject,
                    Body = dto.Body,
                    IsBodyHtml = false
                };

                mail.To.Add(dto.To);

                await smtp.SendMailAsync(mail);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EMAIL ERROR: " + ex.Message);
                throw; // show real error in swagger
            }
        }
    }
}






