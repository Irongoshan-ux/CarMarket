using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserRepository(ApplicationDbContext userContext, IMapper mapper)
        {
            _context = userContext;
            _mapper = mapper;
        }

        public async Task<UserModel> FindUserModelAsync(string email, string password)
        {
            var userEntity = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(x => (x.Email == email) && (x.Password == password));

            var userModel = UserConverter.ToModel(userEntity);

            return userModel; //_mapper.Map<UserModel>(userModel);
        }

        public async Task<UserModel> FindByIdAsync(long id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            return UserConverter.ToModel(userEntity);
        }

        public async Task<List<UserModel>> FindAllAsync()
        {
            var userModels = await _context.Users
                .AsNoTracking()
                .Select(x => UserConverter.ToModel(x)) //_mapper.Map<UserModel>(x))
                .ToListAsync();

            return userModels;
        }

        public async Task<long> SaveAsync(UserModel userModel)
        {
            var newUserEntity = UserConverter.ToEntity(userModel);
            
            var added = await _context.Users.AddAsync(newUserEntity);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }

        public async Task DeleteAsync(long userId)
        {
            var userEntity = await _context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync(); // why isn't working? _userConverter.ToEntity(userModel);

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return UserConverter.ToModel(userEntity);
        }

        public async Task<Role> FindUserRoleAsync(string roleName)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
