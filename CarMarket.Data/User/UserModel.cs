using CarMarket.Core.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Data.User
{
    public class UserModel
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Permission> Permissions { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
