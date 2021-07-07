using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarMarket.Core
{
    public interface IUserService
    {
        public User Get(int id);
        public User GetAll();
        public void Create(User user);
        public void AddPermission(params Permission[] permissions);
        public void AddPermission(Permission permission);
        public void ChangePermission(Permission repcaceablePermission, Permission substitutePermission);
    }
}
