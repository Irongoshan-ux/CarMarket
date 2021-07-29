using CarMarket.Core.Car.Domain;
using CarMarket.Core.RequestFeatures;
using CarMarket.UI.Features;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarMarket.UI.HttpRepository
{
    public class ProductHttpRepository : ICarHttpRepository
    {
        private readonly string API_WEB_PATH = "https://localhost:10001/api/";
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;

        public ProductHttpRepository(HttpClient client)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<PagingResponse<CarModel>> GetProducts(ModelParameters productParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString(),
                ["searchTerm"] = productParameters.SearchTerm == null ? "" : productParameters.SearchTerm,
            };
            var response = await _client
                .GetAsync(QueryHelpers.AddQueryString(API_WEB_PATH + "Car/GetCarsByParameters", queryStringParam));

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<CarModel>
            {
                Items = JsonSerializer.Deserialize<List<CarModel>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task CreateCar(CarModel product)
        {
            var content = JsonSerializer.Serialize(product);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var postResult = await _client.PostAsync(API_WEB_PATH + "Car/CreateCar", bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task<string> UploadCarImage(MultipartFormDataContent content)
        {
            var postResult = await _client.PostAsync(API_WEB_PATH + "upload", content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5001/", postContent);
                return imgUrl;
            }
        }

        public async Task<CarModel> GetCar(long id)
        {
            var url = Path.Combine(API_WEB_PATH + "Car/GetCar/", id.ToString());

            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var product = JsonSerializer.Deserialize<CarModel>(content, _options);
            return product;
        }

        public async Task UpdateCar(CarModel car)
        {
            var content = JsonSerializer.Serialize(car);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var url = Path.Combine(API_WEB_PATH + "Car/UpdateCar/", car.Id.ToString());

            var postResult = await _client.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();

            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
        }

        public async Task DeleteCar(long id)
        {
            var url = Path.Combine(API_WEB_PATH + "Car/DeleteCar", id.ToString());

            var deleteResult = await _client.DeleteAsync(url);
            var deleteContent = await deleteResult.Content.ReadAsStringAsync();

            if (!deleteResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(deleteContent);
            }
        }
    }
}
