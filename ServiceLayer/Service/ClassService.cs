using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.Repository;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ClassService
    {
        private readonly IClassRepository _classRepository;
        public ClassService(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        // 🟣 Add new Class
        public async Task<Class> AddClassAsync(ClassRequestDTO request)
        {
            var @class = new Class
            {
                ClassId = Guid.NewGuid(),
                ClassName = request.ClassName,
                Grade = request.Grade
            };

            return await _classRepository.AddClass(@class);
        }

        // 🟢 Get all Classes
        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            return await _classRepository.GetAllClass();
        }

        // 🟡 Get Class by ID
        public async Task<Class?> GetClassByIdAsync(Guid id)
        {
            return await _classRepository.GetClassById(id);
        }

        // 🔵 Update Class
        public async Task<Class?> UpdateClasssync(Guid id, ClassRequestDTO request)
        {
            var existing = await _classRepository.GetClassById(id);
            if (existing == null) return null;

            existing.ClassName = request.ClassName;
            existing.Grade = request.Grade;
            return await _classRepository.UpdateClass(existing);
        }

        // 🔴 Delete user
        public async Task<bool> DeleteClassAsync(Guid id)
        {
            return await _classRepository.DeleteClass(id);
        }
    }
}

    

