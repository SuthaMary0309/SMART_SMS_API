using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.Repository;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer;
using ServiceLayer.Service;
using ServiceLayer.ServiceInterFace;
using SMART_SMS_API.Swagger;
using System.Linq;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add configuration files BEFORE Build()
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("RepositoryLayer") 
    )
);
// DI Layers  
builder.Services.AddRepositoryLayer(builder.Configuration);
builder.Services.AddServiceLayer();
builder.Services.AddScoped<IJwtService, JwtTokenService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

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
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Email
builder.Services.AddScoped<IEmailService, EmailService>();

// Google Sheets
builder.Services.AddSingleton<GoogleSheetsService>();

// Repositories & Services
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
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
builder.Services.AddScoped<IParentRepository, ParentRepository>();
builder.Services.AddScoped<IParentService, ParentService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Make JSON property names case-insensitive
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SMART SMS API",
        Version = "v1"
    });
    
    // Map IFormFile to binary format for Swagger
    c.MapType<IFormFile>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "binary"
    });
    
    // Use operation filter to handle file uploads in request body
    c.OperationFilter<FileUploadOperationFilter>();
    
    // Resolve any conflicting actions
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


// AI Chat HttpClient
builder.Services.AddHttpClient();

// Build the app
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS **after app is built**
app.UseCors("AllowAngular"); // Angular frontend
// app.UseCors("AllowAll"); // optional, if needed

app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();
