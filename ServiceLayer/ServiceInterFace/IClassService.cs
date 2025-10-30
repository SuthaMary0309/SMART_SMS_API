using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IClassService
    {
        Task<IEnumerable<Class>> GetAllClassesAsync();
        Task<Class?> GetClassByIdAsync(Guid id);
        Task<Class> AddClassAsync(ClassRequestDTO request);
        Task<Class?> UpdateClassAsync(Guid id, ClassRequestDTO request);
        Task<bool> DeleteClassAsync(Guid id);
    }
}
