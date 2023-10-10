using Domain.Entities;

namespace Application.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Staff staff);
        string GenerateRefreshToken(Staff staff);
    }
}
