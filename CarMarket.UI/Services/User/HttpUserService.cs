using CarMarket.Core.Car.Domain;
using CarMarket.Core.DataResult;
using CarMarket.Core.User.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.User
{
    public class HttpUserService : IHttpUserService
    {
        public Task<UserModel> CreateAsync(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CarModel> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<UserModel>> GetByPageAsync(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> SearchAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> UpdateCarAsync(string id, UserModel updatedModel)
        {
            throw new NotImplementedException();
        }
    }
}
