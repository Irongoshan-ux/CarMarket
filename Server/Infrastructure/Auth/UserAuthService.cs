using CarMarket.Core.User.Domain;
using CarMarket.Core.User.JWT;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarMarket.Server.Infrastructure.Auth
{
    public class UserAuthService : IUserAuthService
    {
        public HttpClient _httpClient { get; }

        public UserAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> LoginAsync(UserModel user)
        {
            user.Password = Utility.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Identification/Login");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

            return await Task.FromResult(returnedUser);

        }

        public async Task<UserModel> RegisterUserAsync(UserModel user)
        {
            user.Password = Utility.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Identification/Register");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<UserModel> RefreshTokenAsync(RefreshRequest refreshRequest)
        {
            string serializedUser = JsonConvert.SerializeObject(refreshRequest);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Identification/RefreshToken");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<UserModel> GetUserByAccessTokenAsync(string accessToken)
        {
            string serializedRefreshRequest = JsonConvert.SerializeObject(accessToken);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Identification/GetUserByAccessToken");
            requestMessage.Content = new StringContent(serializedRefreshRequest);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserModel>(responseBody);

            return await Task.FromResult(returnedUser);
        }
    }
}
