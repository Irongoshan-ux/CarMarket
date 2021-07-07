using System.Collections.Generic;

namespace CarMarket.Core
{
    public interface IUserRepository
    {
        public User Get(int id);
        public List<User> GetAll();
    }
}