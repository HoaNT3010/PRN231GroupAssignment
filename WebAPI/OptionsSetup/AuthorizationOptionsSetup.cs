using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace WebAPI.OptionsSetup
{
    public class AuthorizationOptionsSetup : IConfigureOptions<AuthorizationOptions>
    {
        public const string ManagerPolicy = "Manager";
        public const string EmployeePolicy = "Employee";
        public void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(ManagerPolicy, policy =>
            {
                policy.RequireRole(StaffRole.Manager.ToString());
            });
            options.AddPolicy(EmployeePolicy, policy =>
            {
                policy.RequireRole(StaffRole.Employee.ToString());
            });
        }
    }
}
