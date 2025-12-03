using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO;
using ServiceLayer.ServiceInterFace;

namespace SMART_SMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto data)
        {
            var result = await _emailService.SendEmailAsync(data.To, data.Subject, data.Body);

            if (result)
                return Ok(new { message = "Email sent successfully" });

            return BadRequest("Email sending failed");
        }
    }
}