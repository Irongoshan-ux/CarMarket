using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.UI.Services
{
    public interface IHttpService<TModel, TKey>
    {
        Task<TModel> GetAsync(TKey id);
        Task<IEnumerable<TModel>> GetAllAsync();
        Task<DataResult<TModel>> GetByPageAsync(int skip, int take);
        Task<TModel> CreateAsync(TModel model, AuthenticationState authorization);
        Task<TModel> UpdateAsync(TKey id, TModel updatedModel, AuthenticationState authorization);
        Task DeleteAsync(TKey Id, AuthenticationState authorization);
    }
}
