using CarMarket.Core.User.Domain;
using CarMarket.Core.User.JWT;
using System.Threading.Tasks;

namespace CarMarket.Server.Infrastructure.Auth
{
    public interface IUserAuthService
    {
        public Task<UserModel> LoginAsync(UserModel user);
        public Task<UserModel> RegisterUserAsync(UserModel user);
        public Task<UserModel> GetUserByAccessTokenAsync(string accessToken);
        public Task<UserModel> RefreshTokenAsync(RefreshRequest refreshRequest);
    }
}
