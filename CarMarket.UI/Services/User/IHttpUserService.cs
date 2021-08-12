using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarMarket.UI.Services.User
{
    public interface IHttpUserService : IHttpService<UserModel, string>
    {
        Task<IEnumerable<UserModel>> SearchAsync(string userName);
    }
}
