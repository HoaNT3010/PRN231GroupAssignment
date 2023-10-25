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

            // Add services
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ICustomerServices, CustomerServices>();


            return services;
        }
    }
}
