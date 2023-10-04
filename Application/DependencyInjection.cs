using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // Add validators
            services.AddValidatorsFromAssembly(assembly);

            // Add automapper profiles

            // Add services

            return services;
        }
    }
}
