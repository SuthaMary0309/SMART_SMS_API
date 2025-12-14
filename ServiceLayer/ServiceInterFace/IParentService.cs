using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IParentService
    {
        Task<IEnumerable<Parent>> GetAllParentsAsync();
        Task<Parent?> GetParentByIdAsync(Guid id);
        Task<Parent> AddParentAsync(ParentRequestDTO dto);
        Task<Parent?> UpdateParentAsync(Guid id, ParentRequestDTO request);
        Task<bool> DeleteParentAsync(Guid id);
    }
}
