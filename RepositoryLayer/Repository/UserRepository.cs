using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;

namespace RepositoryLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext aplicationDbContext)
        {
            _dbContext = aplicationDbContext;
        }

        public async Task<User> AddUser(User user)
        {
            var response = await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return response.Entity;
        }
    }
}