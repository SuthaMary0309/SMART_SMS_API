using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.Repository;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer;
using ServiceLayer.Service;
using ServiceLayer.ServiceInterFace;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI Layers  
builder.Services.AddRepositoryLayer(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddScoped<IJwtService, JwtTokenService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
// Program.cs (Add)
builder.Services.AddScoped<IReportService, ReportService>();
// ensure DbContext registered already, CORS etc.

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

builder.Services.AddScoped<IEmailService, EmailService>();
 

builder.Services.AddControllers();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IExamRepository, ExamRepository>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IMarksRepository, MarksRepository>();
builder.Services.AddScoped<IMarksService, MarksService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IParentRepository,ParentRepository>();
builder.Services.AddScoped<IParentService, ParentService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// MIDDLEWARE ORDER IMPORTANT
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
// NEW ROLE BASED AUTHONTICATION
//builder.Services.AddAuthorization(options =>
//    options.AddPolicy("TeacherOrAdmin", policy => policy.RequireRole("Teacher", "Admin"));
//});


app.MapControllers();

app.Run();
