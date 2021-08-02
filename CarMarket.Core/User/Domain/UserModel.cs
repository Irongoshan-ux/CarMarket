namespace CarMarket.Core.User.Domain
{
    public class UserModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        //public ICollection<Permission> Permissions { get; set; }
    }
}