using CarMarket.Core.User;
using System.Collections.Generic;
using System.Linq;

namespace CarMarket.Data.User
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _userContext;
        private readonly UserConverter _userConverter;

        public UserRepository(ApplicationDbContext userContext, UserConverter userConverter)
        {
            _userContext = userContext;
            _userConverter = userConverter;
        }

        public UserDto FindById(int id)
        {
            return _userConverter.ToDto(_userContext.Users.FindAsync(id).Result);
        }

        public List<UserDto> FindAll()
        {
            var userModels = _userContext.Users.ToList();
            var usersDto = new List<UserDto>(userModels.Count);

            foreach (var model in userModels)
            {
                usersDto.Add(_userConverter.ToDto(model));
            }

            return usersDto;
        }

        public UserDto FindByEmail(string email)
        {
            return _userConverter.ToDto(_userContext.Users.FindAsync(email).Result);
        }

        public void Save(UserDto userDto)
        {
            var newUser = _userConverter.ToEntity(userDto);

            _userContext.AddAsync(newUser);
        }
    }
}
