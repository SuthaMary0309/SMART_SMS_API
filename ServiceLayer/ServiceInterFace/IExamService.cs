using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IExamService
    {
        Task<IEnumerable<Exam>> GetAllExamsAsync();
        Task<Exam?> GetExamByIdAsync(Guid id);
        Task<Exam> AddExamAsync(ExamRequestDTO request);
        Task<Exam?> UpdateExamAsync(Guid id, ExamRequestDTO request);
        Task<bool> DeleteExamAsync(Guid id);
    }
}
