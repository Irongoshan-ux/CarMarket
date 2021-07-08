using System.Collections.Generic;

namespace CarMarket.Core
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Permission> Permissions { get; set; }
    }
}
