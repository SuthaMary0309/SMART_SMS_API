using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        // 🟣 Add new Class (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddClass(string className, string grade)
        {
            var classDto = new ServiceLayer.DTO.RequestDTO.ClassRequestDTO
            {
                ClassName = className,
                Grade = grade
            };

            var createdClass = await _classService.AddClassAsync(classDto);
            return Ok(createdClass);
        }

        // 🟢 Get all Classes
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _classService.GetAllClassesAsync();
            return Ok(classes);
        }

        // 🟡 Get Class by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var classEntity = await _classService.GetClassByIdAsync(id);
            if (classEntity == null)
                return NotFound(new { message = "Class not found" });

            return Ok(classEntity);
        }

        // 🔵 Update Class (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateClass(Guid id, string className, string grade)
        {
            var classDto = new ServiceLayer.DTO.RequestDTO.ClassRequestDTO
            {
                ClassName = className,
                Grade = grade
            };

            var updated = await _classService.UpdateClassAsync(id, classDto);
            if (updated == null)
                return NotFound(new { message = "Class not found" });

            return Ok(updated);
        }

        // 🔴 Delete Class
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteClass(Guid id)
        {
            var deleted = await _classService.DeleteClassAsync(id);
            if (!deleted)
                return NotFound(new { message = "Class not found or already deleted" });

            return Ok(new { message = "Class deleted successfully" });
        }
    }
}
