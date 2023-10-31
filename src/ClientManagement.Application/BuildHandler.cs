using ClientManagement.Application.Commons;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ClientManagement.Application
{
    public static class BuildHandler
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddDomainMessageHandler();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
