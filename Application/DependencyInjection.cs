﻿using Application.Services.Implementations;
using Application.Services.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Text.Json.Serialization;

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
            services.AddControllers().AddJsonOptions(x =>
              x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            // Add automapper profiles
            services.AddAutoMapper(assembly);

            // Add services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<ICustomerServices, CustomerServices>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IMomoService, MomoService>();


            return services;
        }
    }
}
