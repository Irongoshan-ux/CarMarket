using System.Collections.Generic;

namespace CarMarket.Core.User.Domain
{
    public class UserModel
    {
        public UserModel(string firstName, string lastName, List<Permission> permissions, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Permissions = permissions;
            Email = email;
            Password = password;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Permission> Permissions { get; set; }
        public string Password { get; set; }
    }
}