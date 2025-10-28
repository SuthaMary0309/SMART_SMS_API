using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using ServiceLayer.ServiceInterFace;

namespace ServiceLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUserAsync(string name, int age)
        {
            var userdto = new User
            {
                Id = Guid.NewGuid(),
                UserName = name,
                Age = age
            };
            var addUser = await _userRepository.AddUser(userdto);
            return addUser;
        }
    }
}