
using System.ComponentModel.DataAnnotations;

namespace Entities.DataModels
{
    [Serializable]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
