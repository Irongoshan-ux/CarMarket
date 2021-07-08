using CarMarket.Core.User.Domain;

namespace CarMarket.Core.User.Service
{
    public interface IUserService
    {
        public UserModel Get(int id);
        public UserModel GetAll();
        public void Create(UserModel user);
        public void AddPermission(params Permission[] permissions);
        public void AddPermission(Permission permission);
        public void ChangePermission(Permission repcaceablePermission, Permission substitutePermission);
    }
}
