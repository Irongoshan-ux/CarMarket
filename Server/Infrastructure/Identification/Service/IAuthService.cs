using System.Security.Claims;
using System.Collections.Generic;
using CarMarket.Server.Infrastructure.Identification.Models;

namespace CarMarket.Server.Infrastructure.Identification.Service
{
    public interface IAuthService
    {
        string SecretKey { get; set; }

        bool IsTokenValid(string token);
        string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}
