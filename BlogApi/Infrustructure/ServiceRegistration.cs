using Blog.Application.Abstractions.Configurations;
using Blog.Application.Abstractions.JWT;
using Blog.Application.Abstractions.Services;
using Blog.Application.Abstractions.Storage;
using Blog.Infrustructure.Services;
using Blog.Infrustructure.Services.Configurations;
using Blog.Infrustructure.Services.JWT;
using Blog.Infrustructure.Services.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrustructure
{
    public static class ServiceRegistration
    {
        public static void addInfrustructureServices(this IServiceCollection services) {
            services.AddScoped<ITokenHandler,TokenHandler>();
            services.AddScoped<IStorageService,StorageService>();
            services.AddScoped<IApplicationService,ApplicationService>();
            services.AddScoped<IAuthorizationEndpointService, AuthorizationEndpointService>();
        }

        public static void addStorage<T>(this IServiceCollection serviceCollection) where T : class, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
    }
}
