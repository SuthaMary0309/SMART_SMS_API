using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryInterface
{
    public interface IMarksRepository
    {
        Task<Marks> AddMarks(Marks marks);
        Task<IEnumerable<Marks>> GetAllMarks();
        Task<Marks?> GetMarksById(Guid id);
        Task<Marks?> UpdateMarks(Marks marks);
        Task<bool> DeleteMarks(Guid id);
    }
}
