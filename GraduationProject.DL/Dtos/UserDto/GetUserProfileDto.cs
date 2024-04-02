using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.UserDto
{
    public class GetUserProfileDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Bio { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public List<UserProfilePlace> OwnedPlaces { get; set; }
    }
}
