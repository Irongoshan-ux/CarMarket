﻿using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using CarMarket.UI.Pages;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CarMarket.UI.Services
{
    public class HttpCarService : IHttpCarService
    {
        private readonly HttpClient _httpClient;

        public HttpCarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CarModel> CreateAsync(CarModel userModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Car/CreateCar", userModel);

            return response.Content.ReadFromJsonAsync<CarModel>().Result;
        }

        public async Task DeleteAsync(long carId)
        {
            await _httpClient.DeleteAsync("/api/Car/DeleteCar/" + carId);
        }

        public async Task<IEnumerable<CarModel>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CarModel>>("/api/Car/GetCars");
        }

        public async Task<IEnumerable<CarModel>> GetAllUserCarsAsync(long userId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CarModel>>("/api/Car/GetAllUserCars/" + userId);
        }

        public async Task<CarModel> GetAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<CarModel>("/api/Car/GetCar/" + id);
        }

        public async Task<DataResult<CarModel>> GetByPageAsync(int skip, int take)
{
            return await _httpClient.GetFromJsonAsync<DataResult<CarModel>>($"/api/Car/GetCarsByPage?skip={skip}&take={take}");
        }

        public async Task<IEnumerable<CarModel>> SearchAsync(string carName, CarType? carType)
        {
            // temporarily not working
            return await _httpClient.GetFromJsonAsync<IEnumerable<CarModel>>("/api/Car/Search/" + carName);
        }

        public async Task<CarModel> UpdateCarAsync(long carId, CarModel car)
        {
            var response = await _httpClient.PutAsJsonAsync("/api/Car/UpdateCar/" + carId, car);

            return response.Content.ReadFromJsonAsync<CarModel>().Result;
        }
    }
}