using Application.Services.Implementations;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // Add validators
            services.AddValidatorsFromAssembly(assembly);
            services.AddFluentValidationAutoValidation();

            // Add automapper profiles
            services.AddAutoMapper(assembly);

            // Add services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
