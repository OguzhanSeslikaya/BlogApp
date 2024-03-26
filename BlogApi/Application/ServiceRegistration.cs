using Blog.Application.CQRS.Commands.User.SignIn;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application
{
    public static class ServiceRegistration
    {
        public static void addApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddScoped<IValidator<SignInUserCommandRequest>, SignInUserCommandValidator>();
        }
    }
}
