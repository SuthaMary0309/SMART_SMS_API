using Microsoft.AspNetCore.Mvc;
using ServiceLayer.DTO;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly GoogleSheetsService _sheet;

        public AttendanceController()
        {
            _sheet = new GoogleSheetsService("service-account.json");
        }

        // POST: api/attendance/mark-attendance
        [HttpPost("mark-attendance")]
        public IActionResult MarkAttendance([FromBody] AttendanceDTO dto)
        {
            try
            {
                // dto.Date is already a string from frontend (yyyy-MM-dd)
                _sheet.AddAttendance(dto.StudentName, dto.Date, dto.Status);

                return Ok(new { message = "Attendance marked successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Attendance failed",
                    detail = ex.Message
                });
            }
        }
    }
}
