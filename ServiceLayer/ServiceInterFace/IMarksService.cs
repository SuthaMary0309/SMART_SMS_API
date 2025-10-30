using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IMarksService
    {
        Task<IEnumerable<Marks>> GetAllMarksAsync();
        Task<Marks?> GetMarksByIdAsync(Guid id);
        Task<Marks> AddMarksAsync(MarksRequestDTO request);
        Task<Marks?> UpdateMarksAsync(Guid id, MarksRequestDTO request);
        Task<bool> DeleteMarksAsync(Guid id);
    }
}
