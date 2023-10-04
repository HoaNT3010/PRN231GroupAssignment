using Domain.Entities;

namespace Domain.Interfaces.Services
{
    public interface IJwtService
    {
        string GenerateAccessToken(Staff staff);
        string GenerateRefreshToken(Staff staff);
    }
}
