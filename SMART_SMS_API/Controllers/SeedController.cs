using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SeedController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost("all")]
        public async Task<IActionResult> SeedAllData()
        {
            try
            {
                // Check if data already exists
                if (await _db.Classes.AnyAsync())
                {
                    return Ok(new { message = "Data already exists. Use /api/seed/reset to clear and reseed." });
                }

                // 1. Create Classes
                var classes = new List<Class>
                {
                    new Class { ClassId = Guid.NewGuid(), ClassName = "Grade 10-A", Grade = "10" },
                    new Class { ClassId = Guid.NewGuid(), ClassName = "Grade 10-B", Grade = "10" },
                    new Class { ClassId = Guid.NewGuid(), ClassName = "Grade 11-A", Grade = "11" },
                    new Class { ClassId = Guid.NewGuid(), ClassName = "Grade 11-B", Grade = "11" },
                    new Class { ClassId = Guid.NewGuid(), ClassName = "Grade 12-A", Grade = "12" }
                };
                await _db.Classes.AddRangeAsync(classes);
                await _db.SaveChangesAsync();

                // 2. Create Subjects
                var subjects = new List<Subject>
                {
                    new Subject { SubjectID = Guid.NewGuid(), SubjectName = "Mathematics", ClassID = classes[0].ClassId },
                    new Subject { SubjectID = Guid.NewGuid(), SubjectName = "Physics", ClassID = classes[0].ClassId },
                    new Subject { SubjectID = Guid.NewGuid(), SubjectName = "Chemistry", ClassID = classes[1].ClassId },
                    new Subject { SubjectID = Guid.NewGuid(), SubjectName = "Biology", ClassID = classes[1].ClassId },
                    new Subject { SubjectID = Guid.NewGuid(), SubjectName = "English", ClassID = classes[2].ClassId },
                    new Subject { SubjectID = Guid.NewGuid(), SubjectName = "Tamil", ClassID = classes[2].ClassId },
                    new Subject { SubjectID = Guid.NewGuid(), SubjectName = "Computer Science", ClassID = classes[3].ClassId }
                };
                await _db.Subjects.AddRangeAsync(subjects);
                await _db.SaveChangesAsync();


                // 3. Create Teachers
                var teachers = new List<Teacher>
                {
                    new Teacher { TeacherID = Guid.NewGuid(), TeacherName = "Mr. Rajesh Kumar", Email = "rajesh@edulink.com", PhoneNo = "9876543210", Address = "Chennai, Tamil Nadu" },
                    new Teacher { TeacherID = Guid.NewGuid(), TeacherName = "Mrs. Priya Sharma", Email = "priya@edulink.com", PhoneNo = "9876543211", Address = "Colombo, Sri Lanka" },
                    new Teacher { TeacherID = Guid.NewGuid(), TeacherName = "Mr. Suresh Perera", Email = "suresh@edulink.com", PhoneNo = "9876543212", Address = "Jaffna, Sri Lanka" },
                    new Teacher { TeacherID = Guid.NewGuid(), TeacherName = "Mrs. Lakshmi Devi", Email = "lakshmi@edulink.com", PhoneNo = "9876543213", Address = "Madurai, Tamil Nadu" },
                    new Teacher { TeacherID = Guid.NewGuid(), TeacherName = "Mr. Arun Wickrama", Email = "arun@edulink.com", PhoneNo = "9876543214", Address = "Kandy, Sri Lanka" }
                };
                await _db.Teachers.AddRangeAsync(teachers);
                await _db.SaveChangesAsync();

                // 4. Create Parents
                var parents = new List<Parent>
                {
                    new Parent { ParentID = Guid.NewGuid(), ParentName = "Mr. Selvam", Email = "selvam@gmail.com", PhoneNo = "9871234560", Address = "Chennai" },
                    new Parent { ParentID = Guid.NewGuid(), ParentName = "Mrs. Kamala", Email = "kamala@gmail.com", PhoneNo = "9871234561", Address = "Colombo" },
                    new Parent { ParentID = Guid.NewGuid(), ParentName = "Mr. Fernando", Email = "fernando@gmail.com", PhoneNo = "9871234562", Address = "Jaffna" },
                    new Parent { ParentID = Guid.NewGuid(), ParentName = "Mrs. Meena", Email = "meena@gmail.com", PhoneNo = "9871234563", Address = "Madurai" },
                    new Parent { ParentID = Guid.NewGuid(), ParentName = "Mr. Silva", Email = "silva@gmail.com", PhoneNo = "9871234564", Address = "Kandy" }
                };
                await _db.Parents.AddRangeAsync(parents);
                await _db.SaveChangesAsync();

                // 5. Create Students
                var students = new List<Student>
                {
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Arun Kumar", Email = "arun.student@gmail.com", PhoneNo = "9998887770", Address = "Chennai", ClassID = classes[0].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Priya Lakshmi", Email = "priya.student@gmail.com", PhoneNo = "9998887771", Address = "Colombo", ClassID = classes[0].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Karthik Raja", Email = "karthik.student@gmail.com", PhoneNo = "9998887772", Address = "Jaffna", ClassID = classes[1].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Divya Sharma", Email = "divya.student@gmail.com", PhoneNo = "9998887773", Address = "Madurai", ClassID = classes[1].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Ravi Perera", Email = "ravi.student@gmail.com", PhoneNo = "9998887774", Address = "Kandy", ClassID = classes[2].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Meera Devi", Email = "meera.student@gmail.com", PhoneNo = "9998887775", Address = "Chennai", ClassID = classes[2].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Vijay Kumar", Email = "vijay.student@gmail.com", PhoneNo = "9998887776", Address = "Colombo", ClassID = classes[3].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Anitha Fernando", Email = "anitha.student@gmail.com", PhoneNo = "9998887777", Address = "Jaffna", ClassID = classes[3].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Suresh Silva", Email = "suresh.student@gmail.com", PhoneNo = "9998887778", Address = "Madurai", ClassID = classes[4].ClassId },
                    new Student { StudentID = Guid.NewGuid(), StudentName = "Lakshmi Wickrama", Email = "lakshmi.student@gmail.com", PhoneNo = "9998887779", Address = "Kandy", ClassID = classes[4].ClassId }
                };
                await _db.Students.AddRangeAsync(students);
                await _db.SaveChangesAsync();


                // 6. Create Exams
                var exams = new List<Exam>
                {
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Mid Term - Mathematics", ExamDate = DateTime.Now.AddDays(7), SubjectID = subjects[0].SubjectID },
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Mid Term - Physics", ExamDate = DateTime.Now.AddDays(9), SubjectID = subjects[1].SubjectID },
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Mid Term - Chemistry", ExamDate = DateTime.Now.AddDays(11), SubjectID = subjects[2].SubjectID },
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Mid Term - Biology", ExamDate = DateTime.Now.AddDays(13), SubjectID = subjects[3].SubjectID },
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Mid Term - English", ExamDate = DateTime.Now.AddDays(15), SubjectID = subjects[4].SubjectID },
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Final - Mathematics", ExamDate = DateTime.Now.AddDays(30), SubjectID = subjects[0].SubjectID },
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Final - Physics", ExamDate = DateTime.Now.AddDays(32), SubjectID = subjects[1].SubjectID },
                    new Exam { ExamID = Guid.NewGuid(), ExamName = "Unit Test - Computer Science", ExamDate = DateTime.Now.AddDays(5), SubjectID = subjects[6].SubjectID }
                };
                await _db.Exams.AddRangeAsync(exams);
                await _db.SaveChangesAsync();

                // 7. Create Marks for students
                var random = new Random();
                var marks = new List<Marks>();
                foreach (var student in students)
                {
                    foreach (var exam in exams.Take(5)) // First 5 exams completed
                    {
                        var mark = random.Next(45, 98);
                        marks.Add(new Marks
                        {
                            MarksId = Guid.NewGuid(),
                            StudentID = student.StudentID,
                            ExamID = exam.ExamID,
                            Mark = mark,
                            Grade = mark >= 90 ? 1 : mark >= 80 ? 2 : mark >= 70 ? 3 : mark >= 60 ? 4 : mark >= 50 ? 5 : 6
                        });
                    }
                }
                await _db.Marks.AddRangeAsync(marks);
                await _db.SaveChangesAsync();

                // 8. Create Attendance records
                var attendances = new List<Attendance>();
                var statuses = new[] { "Present", "Present", "Present", "Present", "Absent" }; // 80% present rate
                foreach (var student in students)
                {
                    for (int i = 0; i < 20; i++) // Last 20 days
                    {
                        var classId = student.ClassID ?? classes[0].ClassId;
                        attendances.Add(new Attendance
                        {
                            AttendanceId = Guid.NewGuid(),
                            StudentId = student.StudentID,
                            ClassId = classId,
                            Date = DateTime.Now.AddDays(-i),
                            Day = DateTime.Now.AddDays(-i),
                            Time = DateTime.Now,
                            Status = statuses[random.Next(statuses.Length)]
                        });
                    }
                }
                await _db.Attendances.AddRangeAsync(attendances);
                await _db.SaveChangesAsync();

                return Ok(new
                {
                    message = "Sample data seeded successfully!",
                    data = new
                    {
                        classes = classes.Count,
                        subjects = subjects.Count,
                        teachers = teachers.Count,
                        parents = parents.Count,
                        students = students.Count,
                        exams = exams.Count,
                        marks = marks.Count,
                        attendances = attendances.Count
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message, inner = ex.InnerException?.Message });
            }
        }

        [HttpDelete("reset")]
        public async Task<IActionResult> ResetData()
        {
            try
            {
                // Delete in correct order due to foreign keys
                _db.Marks.RemoveRange(_db.Marks);
                _db.Attendances.RemoveRange(_db.Attendances);
                _db.Exams.RemoveRange(_db.Exams);
                _db.Students.RemoveRange(_db.Students);
                _db.Parents.RemoveRange(_db.Parents);
                _db.Teachers.RemoveRange(_db.Teachers);
                _db.Subjects.RemoveRange(_db.Subjects);
                _db.Classes.RemoveRange(_db.Classes);
                await _db.SaveChangesAsync();

                return Ok(new { message = "All data cleared successfully!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
