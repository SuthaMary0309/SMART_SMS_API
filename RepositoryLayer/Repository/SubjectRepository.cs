using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.RepoInterFace
{
    public interface ISubjectRepository
    {
        Task<Subject> AddSubject(Subject subject);
        Task<IEnumerable<Subject>> GetAllSubjects();
        Task<Subject?> GetSubjectById(Guid id);
        Task<Subject?> UpdateSubject(Subject subject);
        Task<bool> DeleteSubject(Guid id);
    }
}
