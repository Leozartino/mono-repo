using Kuba.Domain.Models;
using System.Security.Claims;

namespace Kuba.Domain.Interfaces
{
    public interface IJwtSettingsHelper
    {
        string GenerateJWT(JwtSettings settings, List<Claim> claims);
    }
}
