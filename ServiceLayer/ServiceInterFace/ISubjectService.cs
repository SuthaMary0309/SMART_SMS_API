using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface ISubjectService
    {
        Task<IEnumerable<Subject>> GetAllSubjectsAsync();
        Task<Subject?> GetSubjectByIdAsync(Guid id);
        Task<Subject> AddSubjectAsync(SubjectRequestDTO request);
        Task<Subject?> UpdateSubjectAsync(Guid id, SubjectRequestDTO request);
        Task<bool> DeleteSubjectAsync(Guid id);
    }
}
