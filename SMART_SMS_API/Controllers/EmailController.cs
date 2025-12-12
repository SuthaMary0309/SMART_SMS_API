using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using SMART_SMS_API.DTOs;
using ServiceLayer.ServiceInterFace;

namespace SMART_SMS_API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _email;

        public EmailController(IEmailService email)
        {
            _email = email;
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] EmailDTO dto)
        {
            var sent = await _email.SendEmailAsync(dto);
            return sent ? Ok("Email Sent") : StatusCode(500, "Error");
        }
    }
}
