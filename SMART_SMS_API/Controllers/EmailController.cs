using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using ServiceLayer.DTO;

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
