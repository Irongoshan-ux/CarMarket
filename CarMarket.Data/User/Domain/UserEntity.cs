﻿using CarMarket.Core.User.Domain;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Data.User.Domain
{
    public class UserEntity : IdentityUser
    {
        //public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //[EmailAddress]
        //public string Email { get; set; }
        //public ICollection<Permission> Permissions { get; set; }
        //public string Password { get; set; }
    }
}
