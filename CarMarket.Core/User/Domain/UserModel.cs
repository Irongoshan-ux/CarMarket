using System.Collections.Generic;

namespace CarMarket.Core.User.Domain
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public string Password { get; set; }
    }
}