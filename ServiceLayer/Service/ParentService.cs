using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.Repository;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ParentService
    {
        private readonly IParentRepository _parentRepository;
        public ParentService(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        // 🟣 Add new Parent
        public async Task<Parent> AddParentAsync(ParentRequestDTO request)
        {
            var parent = new Parent
            {
                ParentID = Guid.NewGuid(),
                ParentName = request.ParentName,
                PhoneNo = request.PhoneNo,
                Address = request.Address,
                Email = request.Email,
                StudentID = request.StudentID,
                UserID = request.UserID,
            };

            return await _parentRepository.AddParent(parent);
        }

        // 🟢 Get all Parents
        public async Task<IEnumerable<Parent>> GetAllParentsAsync()
        {
            return await _parentRepository.GetAllParents();
        }

        // 🟡 Get Parent by ID
        public async Task<Parent?> GetParentByIdAsync(Guid id)
        {
            return await _parentRepository.GetParentById(id);
        }

        // 🔵 Update Parent
        public async Task<Parent?> UpdateParentAsync(Guid id, ParentRequestDTO request)
        {
            var existing = await _parentRepository.GetParentById(id);
            if (existing == null) return null;

            existing.ParentName = request.ParentName;
            existing.PhoneNo = request.PhoneNo;
            existing.Address= request.Address;
            existing.Email = request.Email;
            existing.StudentID = request.StudentID;
            existing.UserID = request. UserID;

            return await _parentRepository.UpdateParent(existing);
        }

        // 🔴 Delete Parent
        public async Task<bool> DeleteParentAsync(Guid id)
        {
            return await _parentRepository.DeleteParent(id);
        }
    }
}

    

