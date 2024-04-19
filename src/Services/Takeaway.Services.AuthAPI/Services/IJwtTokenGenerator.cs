using Takeaway.Services.AuthAPI.Models;

namespace Takeaway.Services.AuthAPI.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
