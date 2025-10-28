using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{

    public static class RepositoryDependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection service, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            service.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(connectionString));

            service.AddScoped<IUserRepository, UserRepository>();
            return service;
        }
    }
}
