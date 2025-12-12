# SMART SMS API - Backend Project Analysis Report

**Generated:** December 10, 2025  
**Project:** Smart School Management System (SMS) API  
**Framework:** ASP.NET Core 8.0  
**Architecture:** 3-Layer Architecture (Presentation, Service, Repository)

---

## 1. Project Overview

The **SMART SMS API** is a School Management System backend built with ASP.NET Core 8.0. It provides RESTful APIs for managing educational institution operations including student enrollment, teacher management, class organization, exam scheduling, marks recording, attendance tracking, and parent communication.

### Solution Structure

```
SMART_SMS_API.sln
├── SMART_SMS_API/           # Presentation Layer (Web API)
│   ├── Controllers/         # 13 API Controllers
│   ├── Program.cs           # Application entry point & DI configuration
│   └── appsettings.json     # Configuration
├── ServiceLayer/            # Business Logic Layer
│   ├── Service/             # 13 Service implementations
│   ├── ServiceInterFace/    # 12 Service interfaces
│   └── DTO/                 # Data Transfer Objects
│       ├── RequestDTO/      # 9 Request DTOs
│       └── ResponseDTO/     # 10 Response DTOs
└── RepositoryLayer/         # Data Access Layer
    ├── Entity/              # 16 Entity models
    ├── Repository/          # 10 Repository implementations
    ├── RepositoryInterface/ # 10 Repository interfaces
    ├── Data/                # DbContext
    └── Migrations/          # EF Core migrations
```

### Key Statistics

| Metric | Count |
|--------|-------|
| Total API Endpoints | **58** |
| Controllers | 13 |
| Entity Models | 16 |
| Services | 13 |
| Repositories | 10 |
| DTOs | 25 |

---

## 2. API Summary

### 2.1 Authentication APIs (`/api/auth`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/auth/register` | POST | `RegisterDTO` (Body): UserName, Email, Password, ConfirmPassword, Role, AdmissionNumber? | `{ message, user: { UserID, UserName, Email, Role } }` | Register new user with auto-generated username |
| `/api/auth/login` | POST | `LoginDTO` (Body): Email, Password | `{ token, role, username }` | Authenticate user and return JWT token |
| `/api/auth/forgot-password` | POST | `ForgotPasswordDTO` (Body): Email | `{ message, token }` | Generate password reset token |
| `/api/auth/reset-password` | POST | `ResetPasswordDTO` (Body): Token, NewPassword | `{ message }` | Reset password using token |
| `/api/auth/stats` | GET | None | `{ Students, Teachers, Exams, Parents }` | Get system statistics counts |

### 2.2 User APIs (`/api/user`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/user/add` | POST | Query: userName, age, email, role | `User` entity | Create new user |
| `/api/user/get-all` | GET | None | `User[]` | Get all users |
| `/api/user/get/{id}` | GET | Path: id (Guid) | `User` | Get user by ID |
| `/api/user/update/{id}` | PUT | Path: id; Query: userName, age, email, role | `User` | Update user |
| `/api/user/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete user |

### 2.3 Student APIs (`/api/student`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/student/add` | POST | `StudentRequestDTO` (Body): StudentName, PhoneNo, Address, Email, ClassID | `Student` | Add new student |
| `/api/student/get-all` | GET | None | `Student[]` | Get all students |
| `/api/student/get/{id}` | GET | Path: id (Guid) | `Student` | Get student by ID |
| `/api/student/update/{id}` | PUT | Path: id; `StudentRequestDTO` (Body) | `Student` | Update student |
| `/api/student/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete student |

### 2.4 Teacher APIs (`/api/teacher`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/teacher/add` | POST | `TeacherRequestDTO` (Body): TeacherName, PhoneNo, Address, Email, UserID | `Teacher` | Add new teacher |
| `/api/teacher/get-all` | GET | None | `Teacher[]` | Get all teachers |
| `/api/teacher/get/{id}` | GET | Path: id (Guid) | `Teacher` | Get teacher by ID |
| `/api/teacher/update/{id}` | PUT | Path: id; `TeacherRequestDTO` (Body) | `Teacher` | Update teacher |
| `/api/teacher/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete teacher |

### 2.5 Parent APIs (`/api/parent`) - **[Authorize]**

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/parent/add` | POST | `ParentRequestDTO` (Body): ParentName, PhoneNo, Address, Email, StudentID | `Parent` | Add parent (UserID from JWT) |
| `/api/parent/all` | GET | None | `Parent[]` | Get all parents |
| `/api/parent/update/{id}` | PUT | Path: id; `ParentRequestDTO` (Body) | `Parent` | Update parent |
| `/api/parent/delete/{id}` | DELETE | Path: id (Guid) | `204 NoContent` | Delete parent |

### 2.6 Class APIs (`/api/Class`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/Class/add` | POST | `ClassRequestDTO` (Body): ClassName, Grade | `Class` | Create new class |
| `/api/Class/get-all` | GET | None | `Class[]` | Get all classes |
| `/api/Class/get/{id}` | GET | Path: id (Guid) | `Class` | Get class by ID |
| `/api/Class/update/{id}` | PUT | Path: id; `ClassRequestDTO` (Body) | `Class` | Update class |
| `/api/Class/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete class |

### 2.7 Subject APIs (`/api/subject`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/subject/add` | POST | `SubjectRequestDTO` (Body): SubjectName, StudentID, ClassID, UserID, TeacherID | `Subject` | Add new subject |
| `/api/subject/get-all` | GET | None | `Subject[]` | Get all subjects |
| `/api/subject/get/{id}` | GET | Path: id (Guid) | `Subject` | Get subject by ID |
| `/api/subject/update/{id}` | PUT | Path: id; `SubjectRequestDTO` (Body) | `Subject` | Update subject |
| `/api/subject/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete subject |

### 2.8 Exam APIs (`/api/Exam`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/Exam/add` | POST | `ExamRequestDTO` (Body): ExamName, ExamDate, SubjectID, ClassID | `Exam` | Schedule new exam |
| `/api/Exam/get-all` | GET | None | `Exam[]` | Get all exams |
| `/api/Exam/get/{id}` | GET | Path: id (Guid) | `Exam` | Get exam by ID |
| `/api/Exam/update/{id}` | PUT | Path: id; `ExamRequestDTO` (Body) | `Exam` | Update exam |
| `/api/Exam/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete exam |

### 2.9 Marks APIs (`/api/Marks`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/Marks/add` | POST | Query: grade, mark, studentID, examID | `Marks` | Record student marks |
| `/api/Marks/get-all` | GET | None | `Marks[]` | Get all marks |
| `/api/Marks/get/{id}` | GET | Path: id (Guid) | `Marks` | Get marks by ID |
| `/api/Marks/update/{id}` | PUT | Path: id; Query: grade, mark, studentID, examID | `Marks` | Update marks |
| `/api/Marks/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete marks |

### 2.10 Notification APIs (`/api/Notification`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/Notification/add` | POST | Query: title, type, message, dateSent, dateReceived, isRead, userID | `Notification` | Create notification |
| `/api/Notification/get-all` | GET | None | `Notification[]` | Get all notifications |
| `/api/Notification/get/{id}` | GET | Path: id (Guid) | `Notification` | Get notification by ID |
| `/api/Notification/update/{id}` | PUT | Path: id; Query params | `Notification` | Update notification |
| `/api/Notification/delete/{id}` | DELETE | Path: id (Guid) | `{ message }` | Delete notification |

### 2.11 Report APIs (`/api/Report`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/Report/student/{studentId}` | GET | Path: studentId (Guid) | `{ studentName, className, highest, lowest, average, marks[] }` | Generate student performance report |
| `/api/Report/exam/{examId}` | GET | Path: examId (Guid) | `{ examName, subjectName, highest, lowest, average, students[] }` | Generate exam results report |
| `/api/Report/class/{classId}/exam/{examId}` | GET | Path: classId, examId (Guid) | `{ className, examName, subjectName, stats, passPercentage, students[] }` | Generate class performance report |

### 2.12 Email APIs (`/api/Email`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/Email/send` | POST | `EmailDto` (Body): To, Subject, Body | `{ message }` | Send email via SMTP |

### 2.13 Google Sheets Test APIs (`/api/test-sheet`)

| Endpoint | Method | Parameters | Response | Description |
|----------|--------|------------|----------|-------------|
| `/api/test-sheet/write` | POST | None | `string` | Write test row to Google Sheet |
| `/api/test-sheet/read` | GET | None | `IList<IList<object>>` | Read data from Google Sheet |

---

## 3. Core Features / Modules

### 3.1 Authentication & Authorization Module
- **JWT-based authentication** with configurable expiration
- **Role-based access control** (Admin, Teacher, Parent, Student)
- **Password hashing** using HMACSHA512
- **Password reset** functionality with time-limited tokens
- **Auto-generated usernames** in format: `SMART_SMS_{ROLE_PREFIX}{AdmissionNumber}`

### 3.2 User Management Module
- CRUD operations for system users
- Role assignment (Admin, Teacher, Parent, Student)
- Email-based identification

### 3.3 Student Management Module
- Student registration with class assignment
- Contact information management
- Linked to User accounts

### 3.4 Teacher Management Module
- Teacher profile management
- User account linkage
- Contact information storage

### 3.5 Parent Management Module
- Parent registration with student linkage
- JWT-based user association
- Contact management

### 3.6 Class Management Module
- Class/Grade organization
- Student-Class relationships

### 3.7 Subject Management Module
- Subject catalog management
- Teacher-Subject assignments
- Class-Subject relationships

### 3.8 Examination Module
- Exam scheduling
- Subject and class association
- Date management

### 3.9 Marks/Grading Module
- Student marks recording
- Grade assignment
- Exam-based mark tracking

### 3.10 Notification Module
- System notifications
- Read/unread status tracking
- User-targeted messaging

### 3.11 Reporting Module
- **Student Report**: Individual performance with marks breakdown
- **Exam Report**: Exam-wise results with statistics
- **Class Performance Report**: Class-wide analysis with pass percentage

### 3.12 Email Service Module
- SMTP-based email sending
- HTML email support
- Gmail integration

### 3.13 Google Sheets Integration Module
- Read/Write operations to Google Sheets
- Service account authentication
- Data export capabilities

---

## 4. Services & Models Overview

### 4.1 Services

| Service | Interface | Description |
|---------|-----------|-------------|
| `JwtTokenService` | `IJwtService` | JWT token generation with claims |
| `UserService` | `IUserService` | User CRUD operations |
| `StudentService` | `IStudentService` | Student management |
| `TeacherService` | `ITeacherService` | Teacher management |
| `ParentService` | `IParentService` | Parent management with JWT user linking |
| `ClassService` | `IClassService` | Class/Grade management |
| `SubjectService` | `ISubjectService` | Subject catalog management |
| `ExamService` | `IExamService` | Exam scheduling |
| `MarksService` | `IMarksService` | Marks recording |
| `NotificationService` | `INotificationService` | Notification management |
| `ReportService` | `IReportService` | Report generation with LINQ queries |
| `EmailService` | `IEmailService` | SMTP email sending |
| `GoogleSheetsService` | N/A (Singleton) | Google Sheets API integration |

### 4.2 Entity Models

| Entity | Primary Key | Key Properties | Relationships |
|--------|-------------|----------------|---------------|
| `User` | `UserID` (Guid) | UserName, Email, PasswordHash, PasswordSalt, Role, ResetToken | Base identity |
| `Student` | `StudentID` (Guid) | StudentName, PhoneNo, Address, Email | → User, → Class |
| `Teacher` | `TeacherID` (Guid) | TeacherName, PhoneNo, Address, Email | → User |
| `Parent` | `ParentID` (Guid) | ParentName, PhoneNo, Address, Email | → User?, → Student? |
| `Class` | `ClassId` (Guid) | ClassName, Grade | ← Students |
| `Subject` | `SubjectID` (Guid) | SubjectName | → Student, → Class, → User, → Teacher |
| `Exam` | `ExamID` (Guid) | ExamName, ExamDate | → Class, → Subject |
| `Marks` | `MarksId` (Guid) | Grade, Mark | → Student, → Exam |
| `Attendance` | `AttendanceId` (Guid) | Date, Day, Time | → Class, → Student, → Teacher |
| `Notification` | `NotificationId` (Guid) | Title, Type, Message, IsRead | → User |

### 4.3 Junction/Bridge Tables

| Entity | Purpose |
|--------|---------|
| `StudentExam` | Student-Exam many-to-many |
| `StudentMarks` | Student-Marks relationship |
| `StudentSubject` | Student-Subject enrollment |
| `StudentTeacher` | Student-Teacher assignment |
| `TeacherClass` | Teacher-Class assignment |
| `TeacherSubject` | Teacher-Subject assignment |

### 4.4 Data Transfer Objects (DTOs)

**Request DTOs:**
- `RegisterDTO`, `LoginDTO`, `ForgotPasswordDTO`, `ResetPasswordDTO`
- `UserRequestDTO`, `StudentRequestDTO`, `TeacherRequestDTO`, `ParentRequestDTO`
- `ClassRequestDTO`, `SubjectRequestDTO`, `ExamRequestDTO`, `MarksRequestDTO`, `NotificationRequestDTO`
- `EmailDto`

**Response DTOs:**
- `UserResponseDTO`, `StudentResponseDTO`, `TeacherResponseDTO`, `ParentResponseDTO`
- `ClassResponseDTO`, `SubjectResponseDTO`, `ExamResponseDTO`, `MarksResponseDTO`, `NotificationResponseDTO`
- `ReportDTO`, `StudentReportDTO`, `ExamReportDTO`, `ClassPerformanceDTO`

---

## 5. Database / Data Layer Summary

### 5.1 Database Configuration
- **Database Engine:** SQL Server (LocalDB/Express)
- **ORM:** Entity Framework Core 9.0.10
- **Connection String:** `Server=localhost\SQLEXPRESS;Database=SmartSMS_01`

### 5.2 DbContext Configuration

```csharp
public class ApplicationDbContext : DbContext
{
    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Parent> Parents { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Marks> Marks { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<TeacherClass> Teacherclasses { get; set; }
    public DbSet<TeacherSubject> TeacherSubjects { get; set; }
    public DbSet<StudentExam> StudentExams { get; set; }
    public DbSet<StudentMarks> StudentMark { get; set; }
    public DbSet<StudentSubject> StudentSubjects { get; set; }
    public DbSet<StudentTeacher> StudentTeachers { get; set; }
}
```

### 5.3 Repository Pattern Implementation

| Repository | Interface | Entity |
|------------|-----------|--------|
| `AuthRepository` | `IAuthRepository` | User (Auth operations) |
| `UserRepository` | `IUserRepository` | User |
| `StudentRepository` | `IStudentRepository` | Student |
| `TeacherRepository` | `ITeacherRepository` | Teacher |
| `ParentRepository` | `IParentRepository` | Parent |
| `ClassRepository` | `IClassRepository` | Class |
| `SubjectRepository` | `ISubjectRepository` | Subject |
| `ExamRepository` | `IExamRepository` | Exam |
| `MarksRepository` | `IMarksRepository` | Marks |
| `NotificationRepository` | `INotificationRepository` | Notification |

### 5.4 Migrations History
1. `20251201130708_1st` - Initial schema
2. `20251205163742_Mary_01` - Schema updates
3. `20251207091911_2ndMigration` - Additional changes
4. `20251210045611_3rdMary` - Recent updates
5. `20251210051546_$thMary` - Latest migration

---

## 6. Dependencies & Libraries

### 6.1 SMART_SMS_API (Web API Layer)

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.AspNetCore.Authentication.JwtBearer` | 8.0.0 | JWT authentication middleware |
| `Microsoft.EntityFrameworkCore.Design` | 9.0.10 | EF Core design-time tools |
| `Swashbuckle.AspNetCore` | 6.6.2 | Swagger/OpenAPI documentation |
| `Google.Apis.Auth` | 1.73.0 | Google authentication |
| `Google.Apis.Sheets.v4` | 1.72.0.3966 | Google Sheets API |

### 6.2 ServiceLayer (Business Logic)

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.Extensions.DependencyInjection` | 9.0.10 | DI abstractions |
| `Google.Apis.Auth` | 1.73.0 | Google authentication |
| `Google.Apis.Sheets.v4` | 1.72.0.3966 | Google Sheets API |

### 6.3 RepositoryLayer (Data Access)

| Package | Version | Purpose |
|---------|---------|---------|
| `Microsoft.EntityFrameworkCore` | 9.0.10 | ORM framework |
| `Microsoft.EntityFrameworkCore.SqlServer` | 9.0.10 | SQL Server provider |
| `Microsoft.EntityFrameworkCore.Tools` | 9.0.10 | EF Core CLI tools |
| `Microsoft.Extensions.Configuration` | 9.0.10 | Configuration abstractions |
| `Microsoft.Extensions.DependencyInjection` | 9.0.10 | DI abstractions |
| `Google.Apis.Auth` | 1.73.0 | Google authentication |
| `Google.Apis.Sheets.v4` | 1.72.0.3966 | Google Sheets API |

### 6.4 Framework
- **.NET 8.0** (LTS)
- **ASP.NET Core 8.0**
- **C# 12**

---

## 7. Notes / Recommendations

### 7.1 Critical Issues (Must Fix)

#### 🔴 Bug: NullReferenceException in AuthController
```csharp
// AuthController.cs - _context is declared but never injected
private readonly ApplicationDbContext _context; // NULL!

// GetStats() will crash
var studentsCount = await _context.Students.CountAsync(); // NullReferenceException
```
**Fix:** Add `ApplicationDbContext context` to constructor and assign to `_context`.

#### 🔴 Security: Hardcoded Secrets
```json
// appsettings.json - NEVER commit secrets!
"Key": "THIS_IS_MY_SECRET_KEY_123456789_SUPER_SECRET",
"AppPassword": "abcd efgh ijkl mnop"
```
**Fix:** Use User Secrets, Azure Key Vault, or environment variables.

#### 🔴 Security: Reset Token Exposed
```csharp
// ForgotPassword returns token in response - security vulnerability!
return Ok(new { message = "Reset token generated.", token });
```
**Fix:** Send token via email only, never in API response.

#### 🔴 Security: Password Hashes Exposed
Services return full `User` entity including `PasswordHash` and `PasswordSalt` in API responses.
**Fix:** Use Response DTOs that exclude sensitive fields.

### 7.2 Security Improvements

| Issue | Severity | Recommendation |
|-------|----------|----------------|
| Missing `[Authorize]` on most endpoints | High | Add authorization to all sensitive endpoints |
| Overly permissive CORS (`AllowAnyOrigin`) | Medium | Restrict to specific origins in production |
| Weak JWT key | Medium | Use 256+ bit cryptographically random key |
| No rate limiting | Medium | Implement rate limiting on auth endpoints |
| No input sanitization | Medium | Add input validation/sanitization |

### 7.3 Code Quality Issues

| Issue | Location | Recommendation |
|-------|----------|----------------|
| Typo in filename | `RepositoryDepencyInjection.cs` | Rename to `RepositoryDependencyInjection.cs` |
| Typo in filename | `ServiceDepencyInjection.cs` | Rename to `ServiceDependencyInjection.cs` |
| Inconsistent folder casing | `ServiceInterFace` | Rename to `ServiceInterface` |
| Duplicate DI registrations | `Program.cs` | Remove duplicate service registrations |
| Duplicate JWT claim | `JwtTokenService.cs` | Remove duplicate `NameIdentifier` claim |
| Inconsistent API parameter binding | Controllers | Standardize to `[FromBody]` for POST/PUT |

### 7.4 Architecture Improvements

| Area | Current State | Recommendation |
|------|---------------|----------------|
| Response format | Inconsistent (entities vs anonymous objects) | Implement standardized API response wrapper |
| Error handling | No global handler | Add global exception handling middleware |
| Logging | Minimal (only AuthController) | Implement structured logging (Serilog) |
| Validation | Partial | Use FluentValidation for comprehensive validation |
| Entity relationships | Missing navigation properties | Configure EF Core relationships properly |
| Junction tables | Using int ID | Use composite keys for many-to-many |
| API versioning | None | Implement API versioning |
| Swagger | Basic | Configure JWT authentication in Swagger UI |

### 7.5 Performance Recommendations

1. **Add pagination** to all `get-all` endpoints
2. **Implement caching** for frequently accessed data (classes, subjects)
3. **Use `AsNoTracking()`** for read-only queries
4. **Add database indexes** on frequently queried columns
5. **Implement async streaming** for large report data

### 7.6 Missing Features

- [ ] Attendance tracking API endpoints (entity exists, no controller)
- [ ] Bulk operations (import students, bulk marks entry)
- [ ] Search/filter functionality
- [ ] Audit logging
- [ ] Health check endpoints
- [ ] API documentation (XML comments)

---

## 8. Summary

The SMART SMS API is a functional school management system with **58 API endpoints** across **13 controllers**, implementing core educational management features. The 3-layer architecture provides good separation of concerns, but the codebase requires attention to security vulnerabilities (exposed secrets, missing authorization), code quality issues (naming inconsistencies, duplicate registrations), and a critical bug in the AuthController's GetStats method.

**Priority Actions:**
1. Fix NullReferenceException in AuthController
2. Move secrets to secure configuration
3. Add authorization to all endpoints
4. Implement Response DTOs to prevent data exposure
5. Standardize API response format

---

*Report generated by Kiro AI Assistant*
