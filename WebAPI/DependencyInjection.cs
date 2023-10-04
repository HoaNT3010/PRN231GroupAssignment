using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPI.Middlewares;
using WebAPI.OptionsSetup;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // Configuring Cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_publicPolicy",
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

            return services;
        }
    }
}
