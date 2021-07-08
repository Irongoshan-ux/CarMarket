using System.Collections.Generic;

namespace CarMarket.Core.User
{
    public interface IUserRepository
    {
        public UserDto FindById(int id);
        public UserDto FindByEmail(string email);
        public void Save(UserDto user);
        public List<UserDto> FindAll();
    }
}