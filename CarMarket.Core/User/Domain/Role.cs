using System.Collections.Generic;
namespace CarMarket.Core.User.Domain
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserModel> Users { get; set; }
        public Role()
        {
            Users = new List<UserModel>();
        }
    }
}
