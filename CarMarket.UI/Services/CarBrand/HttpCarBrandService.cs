using CarMarket.Core.Car.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.CarBrand
{
    public class HttpCarBrandService : IHttpCarBrandService
    {
        private readonly HttpClient _httpClient;
        private readonly HttpAccessTokenSetter _httpAccessTokenSetter;

        public HttpCarBrandService(HttpClient httpClient, HttpAccessTokenSetter httpAccessTokenSetter)
        {
            _httpClient = httpClient;
            _httpAccessTokenSetter = httpAccessTokenSetter;
            _httpAccessTokenSetter.HttpClient = _httpClient;
        }

        public async Task<Brand> AddAsync(Brand brand, CancellationToken token)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            var response = await _httpClient.PostAsJsonAsync("/api/Brand", brand, token);

            if(response.IsSuccessStatusCode)
                return response.Content.ReadFromJsonAsync<Brand>().Result;

            return null;
        }

        public async Task<bool> DeleteAsync(long brandId, CancellationToken token)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            var result = await _httpClient.DeleteAsync("/api/Brand/" + brandId, token);

            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Brand>> GetAllAsync(CancellationToken token)
        { 
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            return await _httpClient.GetFromJsonAsync<IEnumerable<Brand>>("/api/Brand", cancellationToken: token);
        }

        public async Task<Brand> GetAsync(long brandId, CancellationToken cancellationToken)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            return await _httpClient.GetFromJsonAsync<Brand>($"/api/Brand/{brandId}", cancellationToken: cancellationToken);
        }

        public async Task<bool> UpdateAsync(long brandId, Brand brand, CancellationToken cancellationToken)
        {
            await _httpAccessTokenSetter.AddAccessTokenAsync();

            var result = await _httpClient.PutAsJsonAsync("/api/Brand/" + brandId, brand, cancellationToken);

            return result.IsSuccessStatusCode;
        }
    }
}
