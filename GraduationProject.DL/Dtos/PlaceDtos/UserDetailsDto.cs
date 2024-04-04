using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class UserDetailsDto
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Image { get; set; }
        public DateTime CommentDateTime { get; set; }

    }
}
