using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace WebAPI.OptionsSetup
{
    public class SwaggerOptionsSetup : IConfigureOptions<SwaggerGenOptions>
    {
        private const string ApiVersion = "v1";
        private const string ApiTilte = "Store Sale System API";
        private const string SecurityDefinitionName = "bearer";
        private const string SecurityScheme = "bearer";
        private const string SecuritySchemeName = "Authorization";
        private const string SecuritySchemeDescription = "Enter valid Jwt access token";
        private const string SecuritySchemeBearerFormat = "JWT";

        public void Configure(SwaggerGenOptions options)
        {
            // Documentation
            options.SwaggerDoc(ApiVersion, new OpenApiInfo
            {
                Version = ApiVersion,
                Title = ApiTilte,

            });
            // Security scheme definition
            options.AddSecurityDefinition(SecurityDefinitionName, new OpenApiSecurityScheme
            {
                Description = SecuritySchemeDescription,
                Name = SecuritySchemeName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = SecurityScheme,
                BearerFormat = SecuritySchemeBearerFormat
            });
            // Security requirement
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SecurityScheme
                        }
                    },
                    new string[] {}
                }
            });
            // Apply security requirements globally
            //options.OperationFilter<SecurityRequirementsOperationFilter>();
            // Set comments path
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath, true);
        }
    }
}
