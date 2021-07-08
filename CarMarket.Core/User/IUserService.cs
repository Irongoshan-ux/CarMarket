namespace CarMarket.Core.User
{
    public interface IUserService
    {
        public UserDto Get(int id);
        public UserDto GetAll();
        public void Create(UserDto user);
        public void AddPermission(params Permission[] permissions);
        public void AddPermission(Permission permission);
        public void ChangePermission(Permission repcaceablePermission, Permission substitutePermission);
    }
}
