using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.Repository;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ExamService :IExamService
    {
        private readonly IExamRepository _examRepository;
        public ExamService(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        // 🟣 Add new Exam
        public async Task<Exam> AddExamAsync(ExamRequestDTO request)
        {
            var exam = new Exam
            {
                ExamID = Guid.NewGuid(),
                ExamName = request.ExamName,
                ExamDate = request.ExamDate,
                SubjectID = request.SubjectID,
                ClassID = request.ClassID,
            };

            return await _examRepository.AddExam(exam);
        }

        // 🟢 Get all Exam
        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
        {
            return await _examRepository.GetAllExams();
        }

        // 🟡 Get Examr by ID
        public async Task<Exam?> GetExamByIdAsync(Guid id)
        {
            return await _examRepository.GetExamById(id);
        }

        // 🔵 Update Exam
        public async Task<Exam?> UpdateExamAsync(Guid id, ExamRequestDTO request)
        {
            var existing = await _examRepository.GetExamById(id);
            if (existing == null) return null;

            existing.ExamName = request.ExamName;
            existing.ExamDate = request.ExamDate;
            existing.SubjectID = request.SubjectID;
            existing.ClassID = request.ClassID; ;

            return await _examRepository.UpdateExam(existing);
        }

        // 🔴 Delete user
        public async Task<bool> DeleteExamAsync(Guid id)
        {
            return await _examRepository.DeleteExam(id);
        }
    }
}

    

