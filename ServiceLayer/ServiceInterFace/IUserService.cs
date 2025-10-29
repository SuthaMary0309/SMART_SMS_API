using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IUserService
    {
        Task<User> AddUserAsync(string name, int age);
    }
}
