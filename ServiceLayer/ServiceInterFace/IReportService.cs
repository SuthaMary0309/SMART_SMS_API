using System;
using System.Threading.Tasks;
using ServiceLayer.DTO.ResponseDTO;

namespace ServiceLayer.ServiceInterFace
{
    public interface IReportService
    {
        Task<StudentReportDTO?> GenerateStudentReportAsync(Guid studentId);
    }
}
