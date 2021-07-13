using System.Security.Claims;
using System.Collections.Generic;
using CarMarket.BusinessLogic.User.Authentication.Models;

namespace CarMarket.BusinessLogic.User.Authentication.Service
{
    public interface IAuthService
    {
        string SecretKey { get; set; }

        bool IsTokenValid(string token);
        string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}
