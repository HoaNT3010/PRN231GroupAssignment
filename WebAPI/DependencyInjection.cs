using Microsoft.AspNetCore.Authentication.JwtBearer;
using Serilog;
using WebAPI.Middlewares;
using WebAPI.OptionsSetup;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            // Common setups
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();

            // Authorization
            services.AddAuthorization();
            services.ConfigureOptions<AuthorizationOptionsSetup>();

            // Swagger
            services.AddSwaggerGen();
            services.ConfigureOptions<SwaggerOptionsSetup>();

            // Configuring Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: "PublicPolicy",
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                );
            });

            // Configuring JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            // Custom middlewares
            services.AddTransient<GlobalExceptionMiddleware>();

            // Logging
            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });

            return services;
        }
    }
}
