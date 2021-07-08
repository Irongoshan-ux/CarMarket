using CarMarket.Core.User;
using System.Collections.Generic;

namespace CarMarket.BusinessLogic.User
{
    class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
