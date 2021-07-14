using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace CarMarket.Server.Infrastructure.Identification.Models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public string SecretKey { get; set; }
        public string SecurityAlghoritm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; }

        public Claim[] Claims { get; set; }
    }
}
