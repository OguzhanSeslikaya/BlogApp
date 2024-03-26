using Blog.Application.Abstractions.Services;
using Blog.Application.Repositories.Authorize.Endpoint;
using Blog.Application.Repositories.File.BannerFile;
using Blog.Application.Repositories.File.PageFile;
using Blog.Domain.Entities.Identity;
using Blog.Persistence.Contexts;
using Blog.Persistence.Repositories.Authorize.Endpoint;
using Blog.Persistence.Repositories.File.BannerFile;
using Blog.Persistence.Repositories.File.PageFile;
using Blog.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Persistence
{
    public static class ServiceRegistration
    {
        public static void addPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<BlogApiDbContext>(options => options.UseNpgsql("User ID=admin;Password=123;Host=localhost;Port=5432;Database=Blog;"));
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<BlogApiDbContext>();

            services.AddScoped<IPageFileReadRepository,PageFileReadRepository>();
            services.AddScoped<IPageFileWriteRepository,PageFileWriteRepository>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IEndpointReadRepository, EndpointReadRepository>();
            services.AddScoped<IEndpointWriteRepository, EndpointWriteRepository>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IBannerFileReadRepository, BannerFileReadRepository>();
            services.AddScoped<IBannerFileWriteRepository, BannerFileWriteRepository>();
        }
    }
}
