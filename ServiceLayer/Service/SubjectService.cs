using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectsAsync()
        {
            return await _subjectRepository.GetAllSubjects();
        }

        public async Task<Subject?> GetSubjectByIdAsync(Guid id)
        {
            return await _subjectRepository.GetSubjectById(id);
        }

        public async Task<Subject> AddSubjectAsync(SubjectRequestDTO request)
        {
            var subject = new Subject
            {
                SubjectID = Guid.NewGuid(),
                SubjectName = request.SubjectName,
                StudentID = request.StudentID,
                ClassID = request.ClassID,
                UserID = request.UserID,
                TeacherID = request.TeacherID
            };

            return await _subjectRepository.AddSubject(subject);
        }

        public async Task<Subject?> UpdateSubjectAsync(Guid id, SubjectRequestDTO request)
        {
            var existing = await _subjectRepository.GetSubjectById(id);
            if (existing == null) return null;

            existing.SubjectName = request.SubjectName;
            existing.StudentID = request.StudentID;
            existing.ClassID = request.ClassID;
            existing.UserID = request.UserID;
            existing.TeacherID = request.TeacherID;

            return await _subjectRepository.UpdateSubject(existing);
        }

        public async Task<bool> DeleteSubjectAsync(Guid id)
        {
            return await _subjectRepository.DeleteSubject(id);
        }
    }
}
