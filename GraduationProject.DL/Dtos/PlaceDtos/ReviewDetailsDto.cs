using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject.BL.Dtos.PlaceDtos
{
    public class ReviewDetailsDto
    {
        public int ReviewId { get; set; }
        public string Review { get; set; }
        public double Rating { get; set; }
        public string UserId { get; set; }
        public DateTime ReviewDate { get; set; }

        public UserDetailsDto User { get; set; }
    }
}
