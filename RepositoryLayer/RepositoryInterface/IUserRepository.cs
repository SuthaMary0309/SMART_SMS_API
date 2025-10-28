using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RepositoryLayer.RepoInterFace
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
    }
}
