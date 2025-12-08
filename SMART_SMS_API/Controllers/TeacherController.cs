using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacherService.GetAllTeachersAsync();
            return Ok(teachers);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var teacher = await _teacherService.GetTeacherByIdAsync(id);
            return teacher != null ? Ok(teacher) : NotFound();
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] TeacherRequestDTO request)
        {
            var teacher = await _teacherService.AddTeacherAsync(request);
            return Ok(teacher);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TeacherRequestDTO request)
        {
            var teacher = await _teacherService.UpdateTeacherAsync(id, request);
            return teacher != null ? Ok(teacher) : NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _teacherService.DeleteTeacherAsync(id);
            return deleted ? Ok(new { message = "Deleted" }) : NotFound();
        }
    }
}
