using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;

namespace WebAPI.OptionsSetup
{
    public class AuthenticationOptionsSetup : IConfigureOptions<AuthenticationOptions>
    {
        public void Configure(AuthenticationOptions options)
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
