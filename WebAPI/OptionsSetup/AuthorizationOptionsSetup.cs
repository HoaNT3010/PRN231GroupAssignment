using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WebAPI.OptionsSetup
{
    public class AuthorizationOptionsSetup : IConfigureOptions<AuthorizationOptions>
    {
        
        public void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(IdentityData.ManagerPolicyName, policy =>
            {
                policy.RequireRole(StaffRole.Manager.ToString());
            });
            options.AddPolicy(IdentityData.EmployeePolicyName, policy =>
            {
                policy.RequireRole(StaffRole.Employee.ToString());
            });
        }
    }
}
