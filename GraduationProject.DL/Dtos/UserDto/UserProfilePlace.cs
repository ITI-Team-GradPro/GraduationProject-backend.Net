using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.UserDto
{
    public class UserProfilePlace
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double OverAllRating { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public List<string> Images { get; set; }
    }
}
