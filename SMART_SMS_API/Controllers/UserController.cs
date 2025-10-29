using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;


namespace SMART_SMS_API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(string name, int age)
        {
            var data = await _userService.AddUserAsync(name, age);
            return Ok(data);
        }
    }
}