using RepositoryLayer.Entity;
using ServiceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
