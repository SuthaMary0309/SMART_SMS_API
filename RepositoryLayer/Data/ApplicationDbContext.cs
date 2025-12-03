using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System.Security.Claims;
namespace RepositoryLayer.AppDbContext
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).HasColumnType("varbinary(max)");
            modelBuilder.Entity<User>().Property(u => u.PasswordSalt).HasColumnType("varbinary(max)");
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<TeacherClass> Teacherclasses { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<StudentExam> StudentExams { get; set; }
        public DbSet<StudentMarks> StudentMark { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<StudentTeacher> StudentTeachers { get; set; }

    }

}
