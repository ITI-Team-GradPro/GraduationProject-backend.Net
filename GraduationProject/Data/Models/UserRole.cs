using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.Data.Models
{
    public class UserRole
{
        //enum
        public int UserRoleId { get; set; }
        public string Name  { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //Navigation Property
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
