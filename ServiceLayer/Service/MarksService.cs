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
    public class MarksService :IMarksService
    {
        private readonly IMarksRepository _marksRepository;
        public MarksService(IMarksRepository marksRepository)
        {
            _marksRepository = marksRepository;
        }

        // 🟣 Add new Marks
        public async Task<Marks> AddMarksAsync(MarksRequestDTO request)
        {
            var marks = new Marks
            {
                MarksId = Guid.NewGuid(),
                Grade = request.Grade,
                Mark = request.Mark,
                ExamID = request.ExamID,
                StudentID = request.StudentID,
            };

            return await _marksRepository.AddMarks(marks);
        }

        // 🟢 Get all Marks
        public async Task<IEnumerable<Marks>> GetAllMarksAsync()
        {
            return await _marksRepository.GetAllMarks();
        }

        // 🟡 Get Marks by ID
        public async Task<Marks?> GetMarksByIdAsync(Guid id)
        {
            return await _marksRepository.GetMarksById(id);
        }

        // 🔵 Update Marks
        public async Task<Marks?> UpdateMarksAsync(Guid id, MarksRequestDTO request)
        {
            var existing = await _marksRepository.GetMarksById(id);
            if (existing == null) return null;

            existing.Grade = request.Grade;
            existing.Mark = request.Mark;
            existing.ExamID = request.ExamID;
            existing.StudentID =  request.StudentID;

            return await _marksRepository.UpdateMarks(existing);
        }

        // 🔴 Delete user
        public async Task<bool> DeleteMarksAsync(Guid id)
        {
            return await _marksRepository.DeleteMarks(id);
        }
    }
}

    

