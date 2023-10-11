
namespace Entities.DataModels
{
    public class User
    {
        
        public int UserId { get; set; }
        public string Login { get; set; }
        public string? Password { get; set; }
        public List<Role> Roles { get; set; }
        public User()
        {
            Roles = new List<Role>();
        }
    }
}
