﻿using AutoMapper;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Repository;
using CarMarket.Data.User.Domain;
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
                //.Include(u => u.Role)
                .FirstOrDefaultAsync(x => (x.Email == email) && (x.PasswordHash == password));

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<UserModel> FindByIdAsync(string id)
        {
            var userEntity = await _context.Users.FindAsync(id);
            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<List<UserModel>> FindAllAsync()
        {
            var userEntities = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<List<UserModel>>(userEntities);
        }

        public async Task<string> SaveAsync(UserModel userModel)
        {
            var newUserEntity = _mapper.Map<UserEntity>(userModel);
            
            var added = await _context.Users.AddAsync(newUserEntity);
            await _context.SaveChangesAsync();

            return added.Entity.Id;
        }

        public async Task DeleteAsync(string userId)
        {
            var userEntity = await _context.Users
                .Where(x => x.Id == userId)
                .FirstOrDefaultAsync();

            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> FindByEmailAsync(string email)
        {
            var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            return _mapper.Map<UserModel>(userEntity);
        }

        public async Task<Role> FindUserRoleAsync(string roleName)
        {
            return default;
            //return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);
        }

        public async Task UpdateAsync(string userId, UserModel userModel)
        {
            var userEntity = _mapper.Map<UserEntity>(userModel);

            _context.Update(userEntity);

            await _context.SaveChangesAsync();            
        }
    }
}
