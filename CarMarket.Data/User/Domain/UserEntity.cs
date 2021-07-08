using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Data.User.Domain
{
    public class UserEntity
    {
        public UserEntity(string firstName, string lastName, List<Permission> permissions, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Permissions = permissions;
            Password = password;
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public List<Permission> Permissions { get; set; }
        public string Password { get; set; }
    }
}
