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

        public UserModel FindUserModel(string email, string password)
        {
            var userEntity = _context.Users.FirstOrDefault(x => (x.Email == email) && (x.Password == password));

            return _userConverter.ToModel(userEntity);
        }

        public async Task<UserModel> FindByIdAsync(long id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            return _userConverter.ToModel(userEntity);
        }

        public async Task<List<UserModel>> FindAllAsync()
        {
            var userModels = await _context.Users
                .AsNoTracking()
                .Select(x => _userConverter.ToModel(x))
                .ToListAsync();

            return userModels;
        }

        public async Task<long> SaveAsync(UserModel userModel)
        {
            var newUserEntity = _userConverter.ToEntity(userModel);
            
            var added = await _context.Users.AddAsync(newUserEntity);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }

        public async Task DeleteAsync(UserModel userModel)
        {
            var userEntity = await _context.Users.Where(x => x.Id == userModel.Id).FirstOrDefaultAsync(); // why isn't working? _userConverter.ToEntity(userModel);

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return _userConverter.ToModel(userEntity);
        }
    }
}
