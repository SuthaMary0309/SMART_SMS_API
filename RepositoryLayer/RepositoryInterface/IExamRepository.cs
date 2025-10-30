using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryInterface
{
    public interface IExamRepository
    {
        Task<Exam> AddExam(Exam exam);
        Task<IEnumerable<Exam>> GetAllExams();
        Task<Exam?> GetExamById(Guid id);
        Task<Exam?> UpdateExam(Exam exam);
        Task<bool> DeleteExam(Guid id);
    }
}
