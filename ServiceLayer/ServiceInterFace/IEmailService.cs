using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailDto dto);
    Task<bool> SendEmailWithAttachmentAsync(EmailAttachmentDto dto);
}
