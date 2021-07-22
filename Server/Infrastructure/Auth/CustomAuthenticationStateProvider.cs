using Blazored.LocalStorage;
using CarMarket.Core.User.Domain;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarMarket.Server.Infrastructure.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService _localStorageService { get; }
        public IUserAuthService _userAuthService { get; set; }
        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService,
            IUserAuthService userAuthService,
            HttpClient httpClient)
        {
            _localStorageService = localStorageService;
            _userAuthService = userAuthService;
            _httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");

            ClaimsIdentity identity;

            if (accessToken != null && accessToken != string.Empty)
            {
                var user = await _userAuthService.GetUserByAccessTokenAsync(accessToken);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task MarkUserAsAuthenticated(UserModel user)
        {
            await _localStorageService.SetItemAsync("accessToken", user.AccessToken);
            await _localStorageService.SetItemAsync("refreshToken", user.RefreshToken);

            var identity = GetClaimsIdentity(user);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("refreshToken");
            await _localStorageService.RemoveItemAsync("accessToken");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(UserModel user)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (user.Email != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                                {
                                    new Claim(ClaimTypes.Name, user.Email),
                                    new Claim(ClaimTypes.Role, user.Role.RoleName),
                                    new Claim("IsAdmin", CheckRole(user, "Admin")),
                                    new Claim("IsUser", CheckRole(user, "User")),
                                    new Claim("IsGuest", CheckRole(user, "Guest"))
                                }, "apiauth_type");
            }

            return claimsIdentity;
        }

        private string CheckRole(UserModel user, string role) => user.Role.RoleName.Equals(role) ? "true" : "false";
    }
}