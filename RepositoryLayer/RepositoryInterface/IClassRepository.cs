using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryInterface
{
    public interface IClassRepository
    {
        Task<Class> AddClass(Class @class);
        Task<IEnumerable<Class>> GetAllClass();
        Task<Class?> GetClassById(Guid id);
        Task<Class?> UpdateClass(Class @class);
        Task<bool> DeleteClass(Guid id);
    }
}

