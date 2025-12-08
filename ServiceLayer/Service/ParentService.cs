using RepositoryLayer.Entity;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO.RequestDTO;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class ParentService : IParentService
    {
        private readonly IParentRepository _parentRepository;
        public ParentService(IParentRepository parentRepository)
        {
            _parentRepository = parentRepository;
        }

        public async Task<Parent> AddParentAsync(ParentRequestDTO request, Guid? userId)
        {
            var parent = new Parent
            {
                ParentID = Guid.NewGuid(),
                ParentName = request.ParentName,
                PhoneNo = request.PhoneNo,
                Address = request.Address,
                Email = request.Email,
                StudentID = request.StudentID,
                UserID = userId
            };

            return await _parentRepository.AddParent(parent);
        }

        public async Task<IEnumerable<Parent>> GetAllParentsAsync()
        {
            return await _parentRepository.GetAllParents();
        }

        public async Task<Parent?> GetParentByIdAsync(Guid id)
        {
            return await _parentRepository.GetParentById(id);
        }

        public async Task<Parent?> UpdateParentAsync(Guid id, ParentRequestDTO request, Guid? userId)
        {
            var existing = await _parentRepository.GetParentById(id);
            if (existing == null) return null;

            existing.ParentName = request.ParentName;
            existing.PhoneNo = request.PhoneNo;
            existing.Address = request.Address;
            existing.Email = request.Email;
            existing.StudentID = request.StudentID;
            existing.UserID = userId;

            return await _parentRepository.UpdateParent(existing);
        }

        public async Task<bool> DeleteParentAsync(Guid id)
        {
            return await _parentRepository.DeleteParent(id);
        }
    }
}
