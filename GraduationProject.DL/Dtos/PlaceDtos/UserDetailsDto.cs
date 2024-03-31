using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class UserDetailsDto
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public DateTimeOffset CommentDateTime { get; set; }

    }
}
