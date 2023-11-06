using Serilog;
using System.Text.Json.Serialization;
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
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    //allow enum string value in swagger and front-end instead of int value
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
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

            // Configuring JWT and Authentication
            services.AddAuthentication().AddJwtBearer();
            services.ConfigureOptions<AuthenticationOptionsSetup>();
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            // Custom middlewares
            services.AddTransient<GlobalExceptionMiddleware>();

            // Logging
            services.AddLogging(builder =>
            {
                builder.AddSerilog();
            });

            // Configuring Momo options
            services.ConfigureOptions<MomoOptionsSetup>();

            return services;
        }
    }
}
