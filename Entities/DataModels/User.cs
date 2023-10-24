using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Entities.DataModels
{
    [Serializable]
    public class User 
    {
        [Key, ForeignKey("Role")]
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime PasswordExpirationDate { get; set; }
        public bool FirstLogin { get; set; }
        public int RoleId { get; set; }

        public Role UserRole { get; set; }
    }
}
