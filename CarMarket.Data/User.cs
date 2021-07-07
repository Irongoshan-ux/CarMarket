using System.ComponentModel.DataAnnotations;

namespace CarMarket.Data
{
    public class User
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
