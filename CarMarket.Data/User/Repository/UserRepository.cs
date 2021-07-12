using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Data.User.Converter;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<UserModel> FindByIdAsync(int id)
        {
            var userEntity = _context.Users.FindAsync(id).GetAwaiter().GetResult();
            return _userConverter.ToModel(userEntity);
        }

        public async Task<List<UserModel>> FindAllAsync()
        {
            var userModels = _context.Users
                .AsNoTracking()
                .Select(x => _userConverter.ToModel(x))
                .ToListAsync()
                .GetAwaiter()
                .GetResult();

            return userModels;

            //var userModels = new List<UserModel>(userEntities.Count);

            //foreach (var userEntity in userEntities)
            //{
            //    userModels.Add(_userConverter.ToModel(userEntity));
            //}

            //return userModels;
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            var userEntity = _context.Users.FindAsync(email).GetAwaiter().GetResult();
            return _userConverter.ToModel(userEntity);
        }

        public async Task<long> SaveAsync(UserModel userModel)
        {
            var newUserEntity = _userConverter.ToEntity(userModel);
            
            var added = await _context.Users.AddAsync(newUserEntity);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }
    }
}
