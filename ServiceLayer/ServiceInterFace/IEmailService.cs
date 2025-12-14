using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Net;
using ServiceLayer.DTO;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailDTO dto);
    
}
