using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryInterface
{
    public interface IParentRepository
    {
        Task<Parent> AddParent(Parent parent);
        Task<IEnumerable<Parent>> GetAllParents();
        Task<Parent?> GetParentById(Guid id);
        Task<Parent?> UpdateParent(Parent parent);
        Task<bool> DeleteParent(Guid id);
    }
}
