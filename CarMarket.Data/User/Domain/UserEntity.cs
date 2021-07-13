using CarMarket.Core.User.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Data.User.Domain
{
    public class UserEntity
    {
        public UserEntity(long id, string firstName, string lastName, ICollection<Permission> permissions, string email, string password)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Permissions = permissions;
            Password = password;
        }

        public UserEntity()
        {

        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public string Password { get; set; }
    }
}
