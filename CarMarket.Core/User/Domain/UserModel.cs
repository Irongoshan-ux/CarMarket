using System.Collections.Generic;

namespace CarMarket.Core.User.Domain
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(long id, string firstName, string lastName, ICollection<Permission> permissions, string email, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Permissions = permissions;
            Email = email;
            Password = password;
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public string Password { get; set; }
    }
}