using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace CarMarket.UI.Services.Extensions
{
    public static class HttpServiceExtension
    {
        public static async Task<T> GetJsonAsync<T>(this HttpClient httpClient, string url, AuthenticationState authorization)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorization.User.Identity.Name);

            var response = await httpClient.SendAsync(request);
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            return JsonSerializer.Deserialize<T>(responseBytes, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }

        public static async Task<HttpResponseMessage> DeleteAsync(this HttpClient httpClient, string url, AuthenticationState authorization)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorization.User.Identity.Name);

            var response = await httpClient.SendAsync(request);

            return new HttpResponseMessage(response.StatusCode);
        }

        /// <summary>
        /// !!! Now not working cause need to add Value to request bode !!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient httpClient, string url, T value, AuthenticationState authorization)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorization.User.Identity.Name);

            var response = await httpClient.SendAsync(request);

            return new HttpResponseMessage(response.StatusCode)
            {
                Content = response.Content
            };
        }

        /// <summary>
        /// !!! Now not working cause need to add Value to request bode !!!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClient"></param>
        /// <param name="url"></param>
        /// <param name="value"></param>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient httpClient, string url, T value, AuthenticationState authorization)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorization.User.Identity.Name);

            var response = await httpClient.SendAsync(request);

            return new HttpResponseMessage(response.StatusCode)
            {
                Content = response.Content
            };
        }
    }
}
