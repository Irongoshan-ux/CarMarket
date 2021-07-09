using CarMarket.Core.User.Domain;
using System.Collections.Generic;

namespace CarMarket.Core.User.Service
{
    public interface IUserService
    {
        public UserModel Get(int id);
        public List<UserModel> GetAll();
        public bool Create(UserModel user);
        public void AddPermission(params Permission[] permissions);
        public void AddPermission(Permission permission);
        public void ChangePermission(Permission repcaceablePermission, Permission substitutePermission);
    }
}
