using System.Collections.Generic;
using CarMarket.Core.User.Domain;

namespace CarMarket.Core.User.Repository
{
    public interface IUserRepository
    {
        public UserModel FindById(int id);
        public UserModel FindByEmail(string email);
        public bool Save(UserModel user);
        public List<UserModel> FindAll();
    }
}