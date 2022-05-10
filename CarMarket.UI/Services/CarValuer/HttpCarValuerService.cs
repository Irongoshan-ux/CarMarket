using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.CarValuer
{
    public class HttpCarValuerService : IHttpCarValuerService
    {
        private readonly HttpClient _httpClient;

        public HttpCarValuerService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CostPerYearResult> AddAsync(CarViewModel model, CancellationToken token)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/CarValuer", model, token);
            
            return response.Content.ReadFromJsonAsync<CostPerYearResult>().Result;
        }

        public Task<CostPerYearResult> GetAsync(long carId, CancellationToken cancellationToken)
        {
            return _httpClient.GetFromJsonAsync<CostPerYearResult>($"/api/CarValuer/{carId}", cancellationToken);
        }
    }
}
