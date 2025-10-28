using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Service;
using ServiceLayer.ServiceInterFace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class ServiceDepencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            return service;
        }
    }
}
