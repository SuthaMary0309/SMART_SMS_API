using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailDTO dto);
    Task<bool> SendEmailWithAttachmentAsync(EmailAttachmentDTO dto);
}
