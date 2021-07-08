using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Data.User.Converter;
using System.Collections.Generic;
using System.Linq;

namespace CarMarket.Data.User.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserConverter _userConverter;

        public UserRepository(ApplicationDbContext userContext, UserConverter userConverter)
        {
            _context = userContext;
            _userConverter = userConverter;
        }

        public UserModel FindById(int id)
        {
            var userEntity = _context.Users.FindAsync(id).GetAwaiter().GetResult();
            return _userConverter.ToModel(userEntity);
        }

        public List<UserModel> FindAll()
        {
            var userEntities = _context.Users.ToList();
            var userModels = new List<UserModel>(userEntities.Count);

            foreach (var userEntity in userEntities)
            {
                userModels.Add(_userConverter.ToModel(userEntity));
            }

            return userModels;
        }

        public UserModel FindByEmail(string email)
        {
            var userEntity = _context.Users.FindAsync(email).GetAwaiter().GetResult();
            return _userConverter.ToModel(userEntity);
        }

        public bool Save(UserModel userModel)
        {
            var newUserEntity = _userConverter.ToEntity(userModel);
            return _context.AddAsync(newUserEntity).IsCompleted;
        }
    }
}
