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
    }
}
