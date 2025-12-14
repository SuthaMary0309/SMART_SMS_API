// Controllers/AttendanceController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using ServiceLayer.DTO;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IGoogleSheetService _sheet;

        public AttendanceController(ApplicationDbContext context, IGoogleSheetService sheet)
        {
            _context = context;
            _sheet = sheet;
        }

        // GET: api/attendance/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.Attendances
                .AsNoTracking()
                .Select(a => new {
                    a.AttendanceId,
                    a.StudentId,
                    a.TeacherId,
                    a.ClassId,
                    Date = a.Date,
                    Time = a.Time != default(DateTime) ? a.Time.ToString("HH:mm") : null,
                    Status = a.Status
                }).ToListAsync();

            // optionally include student/teacher/class names if you want:
            // left as-is; front-end will read student/teacher/class via their APIs or expand here using Includes

            return Ok(list);
        }

        // POST: api/attendance/add
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AttendanceRequestDTO req)
        {
            // basic validation
            if (req == null) return BadRequest("Invalid payload");
            if (!DateTime.TryParse(req.Date, out var date)) return BadRequest("Invalid date format");

            // Duplicate check in DB: same studentId + same date
            var existsDb = await _context.Attendances
                .AnyAsync(a => a.StudentId == req.StudentId && a.Date.Date == date.Date);

            if (existsDb)
                return Conflict("Attendance already exists for this student on that date (DB).");

            // Duplicate check in Google Sheet rows (read a sensible range; adapt sheet name & range)
            try
            {
                // read first 1000 rows from Sheet1 (adjust range if needed)
                var rows = await _sheet.ReadRowsAsync("Sheet1!A2:I1000");
                if (rows != null)
                {
                    foreach (var r in rows)
                    {
                        // we assume sheet columns order when appended (see Append below)
                        // 0: StudentName, 1: StudentId, 2: TeacherName, 3: TeacherId, 4: ClassName, 5: ClassId, 6: Date (yyyy-MM-dd), 7: Time, 8: Status
                        if (r.Count >= 7)
                        {
                            var sheetStudentId = r.Count > 1 ? (r[1]?.ToString() ?? "") : "";
                            var sheetDate = r[6]?.ToString() ?? "";
                            if (!string.IsNullOrEmpty(sheetStudentId) && sheetStudentId == req.StudentId.ToString()
                                && sheetDate == date.ToString("yyyy-MM-dd"))
                            {
                                return Conflict("Attendance already exists for this student on that date (Sheet).");
                            }
                        }
                    }
                }
            }
            catch
            {
                // reading sheet failed — continue to save to DB
            }

            // Save to DB
            var att = new RepositoryLayer.Entity.Attendance
            {
                AttendanceId = Guid.NewGuid(),
                StudentId = req.StudentId,
                TeacherId = req.TeacherId,
                ClassId = req.ClassId,
                Date = date,
                Time = DateTime.TryParse(req.Time, out var tt) ? tt : DateTime.MinValue,
                // add a new Status property in entity if not already present (we'll assume exists)
                Status = req.Status
            };

            _context.Attendances.Add(att);
            await _context.SaveChangesAsync();

            // Append to Google Sheet
            try
            {
                var row = new List<object>{
                    req.StudentName ?? "",
                    req.StudentId.ToString(),
                    req.TeacherName ?? "",
                    req.TeacherId.ToString(),
                    req.ClassName ?? "",
                    req.ClassId.ToString(),
                    date.ToString("yyyy-MM-dd"),
                    req.Time ?? DateTime.Now.ToString("HH:mm"),
                    req.Status ?? ""
                };

                await _sheet.AppendRowAsync("Sheet1!A2:I2", row);
            }
            catch
            {
                // If append fails, record is still saved in DB
            }

            return Ok(new { message = "Attendance recorded", attendanceId = att.AttendanceId });
        }

        // DELETE: api/attendance/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var att = await _context.Attendances.FindAsync(id);
            if (att == null) return NotFound("Attendance not found");

            _context.Attendances.Remove(att);
            await _context.SaveChangesAsync();

            // NOTE: can't delete from Google Sheets easily unless you track row index.
            // You could flag a "deleted" column in sheet or rebuild sheet from DB periodically.
            // We'll keep it simple: delete from DB only.

            return Ok(new { message = "Deleted" });
        }
    }
}
